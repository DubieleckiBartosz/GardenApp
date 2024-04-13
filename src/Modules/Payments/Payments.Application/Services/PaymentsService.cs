using BuildingBlocks.Application.Wrappers;
using Payments.Application.Models.Responses;
using Payments.Domain.SubPayments.ValueTypes;

namespace Payments.Application.Services;

public class PaymentsService : IPaymentsService
{
    private readonly ILogger _logger;
    private readonly ICurrentUser _currentUser;
    private readonly IStripeClient _stripeClient;
    private readonly IPaymentsUnitOfWork _paymentsUnitOfWork;
    private readonly IPaymentsEmailService _paymentsEmailService;

    public PaymentsService(
        ILogger logger,
        ICurrentUser currentUser,
        IStripeClient stripeClient,
        IPaymentsUnitOfWork paymentsUnitOfWork,
        IPaymentsEmailService paymentsEmailService)
    {
        _logger = logger;
        _currentUser = currentUser;
        _stripeClient = stripeClient;
        _paymentsUnitOfWork = paymentsUnitOfWork;
        _paymentsEmailService = paymentsEmailService;
    }

    public async Task CreateNewPayer()
    {
        var userId = _currentUser.UserId;
        var payer = await _paymentsUnitOfWork.PayerRepository.GetPayerByUserIdNTAsync(userId);
        if (payer != null)
        {
            throw new NotFoundException(PaymentsErrors.PayerNotFound(_currentUser.UserId));
        }

        var createOptions = new CustomerCreateOptions
        {
            Email = _currentUser.Email,
            Name = $"{_currentUser.UserName}",
            Metadata = new()
            {
                ["user_id"] = userId,
                ["app_source"] = "GardenApp"
            }
        };

        var service = new CustomerService(_stripeClient);
        var createdStripeCustomer = await service.CreateAsync(createOptions);

        _logger.Warning($"Customer created. [StripeCustomerId: {createdStripeCustomer.Id}]");

        var newPayer = Payer.Create(userId, _currentUser.Email, createdStripeCustomer.Id);
        await _paymentsUnitOfWork.PayerRepository.CreateAsync(newPayer);
        await _paymentsUnitOfWork.SaveAsync();
    }

    public async Task<Response> CancelSubscription()
    {
        var payer = await _paymentsUnitOfWork.PayerRepository.GetPayerByUserIdAsync(_currentUser.UserId);
        if (payer == null)
        {
            throw new NotFoundException(PaymentsErrors.PayerNotFound(_currentUser.UserId));
        }

        var subPayment = await _paymentsUnitOfWork.SubPaymentRepository.GetActiveSubByPayerIdAsync(payer.Id);
        if (subPayment == null)
        {
            throw new NotFoundException(PaymentsErrors.SubPaymentNotFound(_currentUser.UserId, payer.Id));
        }

        subPayment.SetStatus(SubPaymentStatus.PendingCancellation);

        var subscriptionService = new SubscriptionService(_stripeClient);
        subscriptionService.Cancel(subPayment.CustomerSubscriptionId);

        _logger.Warning($"Subscription pending cancellation. [SubscripitonId: {subPayment.CustomerSubscriptionId}]");
        await _paymentsUnitOfWork.SaveAsync();

        return Response.Ok();
    }

    public async Task ContinueCanceledSubscription()
    {
        var payer = await _paymentsUnitOfWork.PayerRepository.GetPayerByUserIdAsync(_currentUser.UserId);
        if (payer == null)
        {
            throw new NotFoundException(PaymentsErrors.PayerNotFound(_currentUser.UserId));
        }

        var subPayment = await _paymentsUnitOfWork.SubPaymentRepository.GetActiveSubByPayerIdAsync(payer.Id);
        if (subPayment == null)
        {
            throw new NotFoundException(PaymentsErrors.SubPaymentNotFound(_currentUser.UserId, payer.Id));
        }

        subPayment.SetStatus(SubPaymentStatus.Active);
        var subscriptionService = new SubscriptionService(_stripeClient);
        var options = new SubscriptionUpdateOptions
        {
            CancelAtPeriodEnd = false,
        };

        subscriptionService.Update(subPayment.CustomerSubscriptionId, options);

        _logger.Warning($"Subscription renewed. [SubscripitonId: {subPayment.CustomerSubscriptionId}]");
        await _paymentsUnitOfWork.SaveAsync();
    }

    public async Task<Response<CreateCheckoutSessionResponse>> CreateCheckoutAsync(CreateCheckoutSessionParameters parameters, string baseUrl)
    {
        var payer = await _paymentsUnitOfWork.PayerRepository.GetPayerByUserIdNTAsync(_currentUser.UserId);
        if (payer == null)
        {
            throw new NotFoundException(PaymentsErrors.PayerNotFound(_currentUser.UserId));
        }

        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = parameters.PriceId,
                    Quantity = 1,
                },
            },
            Mode = "subscription",
            Customer = payer.StripeCustomerId,
            PaymentMethodTypes = new List<string>()
            {
                "card"
            },
            //https://docs.stripe.com/api/checkout/sessions/create#create_checkout_session-expires_at
            ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            SuccessUrl = baseUrl + $"/success",
            CancelUrl = baseUrl,
        };
        var service = new SessionService(_stripeClient);
        var session = await service.CreateAsync(options);
        var sessionId = session.Id;

        var paymentSession = payer.CreatePaymentSession(payer.Id, sessionId, session.SubscriptionId);
        await _paymentsUnitOfWork.PaymentSessionRepository.CreateAsync(paymentSession);

        _logger.Warning($"Session created. [SessionId: {sessionId}, SubscripitonId: {session.SubscriptionId}]");
        await _paymentsUnitOfWork.SaveAsync();

        return Response<CreateCheckoutSessionResponse>.Ok(new(session.Url));
    }

    public async Task SessionCompleted(Session session)
    {
        var result = await _paymentsUnitOfWork.PaymentSessionRepository.GetPaymentBySessionIdAsync(session.Id);
        if (result == null)
        {
            throw new NotFoundException(PaymentsErrors.SessionNotFound(session.Id));
        }

        await CreateSubscriptionPayment(session.SubscriptionId);

        await _paymentsEmailService.SendEmailAsync(new() { session.CustomerEmail }, PaymentTemplateType.Success);
    }

    private async Task CreateSubscriptionPayment(string subscriptionId)
    {
        var subscriptionService = new SubscriptionService(_stripeClient);
        var subscription = await subscriptionService.GetAsync(subscriptionId);
        var nextPaymentDate = subscription.CurrentPeriodEnd;

        _logger.Information($"The next payment date is {nextPaymentDate} [SubscriptionId: {subscriptionId}]");

        var payer = await _paymentsUnitOfWork.PayerRepository.GetPayerByStripeCustomerIdNTAsync(subscription.CustomerId);
        var newSubscriptionPayment = payer!.CreatePaymentSubscription(subscriptionId, nextPaymentDate);

        await _paymentsUnitOfWork.SubPaymentRepository.CreateAsync(newSubscriptionPayment);
        await _paymentsUnitOfWork.SaveAsync();
    }
}