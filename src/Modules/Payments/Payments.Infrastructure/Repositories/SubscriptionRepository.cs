namespace Payments.Infrastructure.Repositories;

internal class SubscriptionRepository : ISubscriptionRepository
{
    private readonly DbSet<Subscription> _subscriptions;

    public SubscriptionRepository(PaymentsContext paymentsContext)
    {
        _subscriptions = paymentsContext.Subscriptions;
    }

    public async Task CreateAsync(Subscription subscription)
    {
        await _subscriptions.AddAsync(subscription);
    }
}