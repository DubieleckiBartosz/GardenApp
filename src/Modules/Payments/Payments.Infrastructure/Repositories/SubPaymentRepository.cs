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
}