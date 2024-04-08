namespace Payments.Infrastructure.Repositories;

internal class PaymentSessionRepository : IPaymentSessionRepository
{
    private readonly DbSet<PaymentSession> _paymentSessions;

    public PaymentSessionRepository(PaymentsContext paymentsContext)
    {
        _paymentSessions = paymentsContext.PaymentSessions;
    }

    public async Task CreateAsync(PaymentSession paymentSession)
    {
        await _paymentSessions.AddAsync(paymentSession);
    }

    public async Task<PaymentSession?> GetPaymentBySessionIdAsync(string sessionId)
    {
        return await _paymentSessions.FirstOrDefaultAsync(_ => _.SessionId == sessionId);
    }
}