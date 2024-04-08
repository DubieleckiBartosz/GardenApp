namespace Payments.Application.Interfaces.Services;

public interface IPaymentsService
{
    Task CreateNewPayer();

    Task<string> CreateCheckoutAsync(CreateCheckoutSessionParameters parameters, string baseUrl);

    Task SessionCompleted(Session session);
}