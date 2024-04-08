namespace Payments.Application.Interfaces.Repositories;

public interface IPaymentSessionRepository
{
    Task CreateAsync(PaymentSession paymentSession);

    Task<PaymentSession?> GetPaymentBySessionIdAsync(string sessionId);
}