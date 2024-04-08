namespace Payments.Infrastructure.Repositories;

internal class PaymentsUnitOfWork : IPaymentsUnitOfWork
{
    private readonly PaymentsContext _paymentsContext;
    private IPayerRepository? _payerRepository;
    private IPaymentSessionRepository? _paymentSessionRepository;
    private ISubscriptionRepository? _subscriptionRepository;
    private ISubPaymentRepository _subPaymentRepository;

    public PaymentsUnitOfWork(PaymentsContext paymentsContext)
    {
        _paymentsContext = paymentsContext;
    }

    public IPayerRepository PayerRepository
    {
        get
        {
            if (this._payerRepository == null)
            {
                this._payerRepository = new PayerRepository(_paymentsContext);
            }

            return this._payerRepository;
        }
    }

    public IPaymentSessionRepository PaymentSessionRepository
    {
        get
        {
            if (this._paymentSessionRepository == null)
            {
                this._paymentSessionRepository = new PaymentSessionRepository(_paymentsContext);
            }

            return this._paymentSessionRepository;
        }
    }

    public ISubscriptionRepository SubscriptionRepository
    {
        get
        {
            if (this._subscriptionRepository == null)
            {
                this._subscriptionRepository = new SubscriptionRepository(_paymentsContext);
            }

            return this._subscriptionRepository;
        }
    }

    public ISubPaymentRepository SubPaymentRepository
    {
        get
        {
            if (this._subPaymentRepository == null)
            {
                this._subPaymentRepository = new SubPaymentRepository(_paymentsContext);
            }

            return this._subPaymentRepository;
        }
    }

    public async Task SaveAsync(CancellationToken cancellation = default)
    {
        await _paymentsContext.SaveAsync(cancellation);
    }
}