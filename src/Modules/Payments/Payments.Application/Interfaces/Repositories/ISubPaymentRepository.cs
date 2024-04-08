using Payments.Domain.SubPayments;

namespace Payments.Application.Interfaces.Repositories;

public interface ISubPaymentRepository
{
    Task CreateAsync(SubPayment paymentSession);

    Task<SubPayment?> GetActiveSubByPayerIdAsync(int payerId);
}