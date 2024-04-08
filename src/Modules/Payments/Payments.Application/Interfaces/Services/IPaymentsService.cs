namespace Payments.Application.Interfaces.Services;

public interface IPaymentsService
{
    Task CreateNewPayer();

    Task CancelSubscription();

    Task ContinueCanceledSubscription();

    Task<string> CreateCheckoutAsync(CreateCheckoutSessionParameters parameters, string baseUrl);

    Task SessionCompleted(Session session);
}