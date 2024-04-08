namespace Payments.Application.Interfaces.Repositories;

public interface ISubscriptionRepository
{
    Task CreateAsync(Subscription subscription);
}