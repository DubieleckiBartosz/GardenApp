using Payments.Domain.SubPayments.ValueTypes;

namespace Payments.Infrastructure.Repositories;

internal class SubPaymentRepository : ISubPaymentRepository
{
    private readonly DbSet<SubPayment> _subscriptionPayments;

    public SubPaymentRepository(PaymentsContext paymentsContext)
    {
        _subscriptionPayments = paymentsContext.SubPayments;
    }

    public async Task CreateAsync(SubPayment paymentSession)
    {
        await _subscriptionPayments.AddAsync(paymentSession);
    }

    public async Task<SubPayment?> GetActiveSubByPayerIdAsync(int payerId)
        => await _subscriptionPayments.FirstOrDefaultAsync(_ => _.PayerId == payerId && _.Status == SubPaymentStatus.Active
            || _.Status == SubPaymentStatus.PendingCancellation);
}