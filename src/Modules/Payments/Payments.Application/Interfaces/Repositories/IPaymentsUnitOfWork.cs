namespace Payments.Application.Interfaces.Repositories;

public interface IPaymentsUnitOfWork
{
    IPayerRepository PayerRepository { get; }
    IPaymentSessionRepository PaymentSessionRepository { get; }
    ISubscriptionRepository SubscriptionRepository { get; }
    ISubPaymentRepository SubPaymentRepository { get; }

    Task SaveAsync(CancellationToken cancellation = default);
}