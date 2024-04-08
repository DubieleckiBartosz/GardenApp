namespace Payments.Application.Services;

internal class ProcessService : IProcessService
{
    private readonly IPaymentsService _paymentsService;
    private readonly ILogger _logger;

    public ProcessService(IPaymentsService paymentsService, ILogger logger)
    {
        _paymentsService = paymentsService;
        _logger = logger;
    }

    public async Task StatusProcess(Event @event)
    {
        switch (@event.Type)
        {
            case Events.CheckoutSessionCompleted:
                var checkoutSession = @event.Data.Object as Session;
                _logger.Information($"Checkout.Session ID: {checkoutSession!.Id}, Status: {checkoutSession.Status}");

                if (checkoutSession.Status == "complete")
                {
                    try
                    {
                        await _paymentsService.SessionCompleted(checkoutSession);
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

            case Events.InvoiceCreated:
                break;

            case Events.InvoiceFinalized:
                break;

            case Events.InvoicePaymentSucceeded:
                var invoice = @event.Data.Object as Invoice;

                break;

            case Events.InvoicePaymentFailed:
                break;

            default:
                _logger.Warning("Unhandled event type: {StripeEvent}", @event.Type);
                break;
        }
    }
}