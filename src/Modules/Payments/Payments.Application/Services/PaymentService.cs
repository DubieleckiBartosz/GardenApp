namespace Payments.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentsEmailService _paymentsEmailService;
    private readonly ILogger _logger;

    public PaymentService(IPaymentsEmailService paymentsEmailService, ILogger logger)
    {
        _paymentsEmailService = paymentsEmailService;
        _logger = logger;
    }

    public async Task StatusProcess(Event @event)
    {
        switch (@event.Type)
        {
            case Events.CheckoutSessionCompleted:
                var checkoutSession = @event.Data.Object as Session;
                _logger.Information("Checkout.Session ID: {CheckoutId}, Status: {CheckoutSessionStatus}", checkoutSession!.Id, checkoutSession.Status);

                if (checkoutSession.Status == "complete" && checkoutSession.PhoneNumberCollection.Enabled)
                {
                    try
                    {
                        await _paymentsEmailService.SendEmailAsync(new() { "test@mail.com" }, PaymentTemplateType.Success);
                    }
                    catch (Exception ex)
                    {
                        _logger.Fatal(ex, ex.Message);
                    }
                }

                break;

            case Events.CheckoutSessionExpired:
                break;

            case Events.CheckoutSessionAsyncPaymentSucceeded:
                break;

            case Events.CustomerSubscriptionCreated:
                break;

            case Events.CustomerSubscriptionUpdated:
                break;

            case Events.CustomerSubscriptionDeleted:
                break;

            default:
                _logger.Warning("Unhandled event type: {StripeEvent}", @event.Type);
                break;
        }
    }
}