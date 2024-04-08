namespace Payments.Application.Interfaces.Repositories;

public interface IPayerRepository
{
    Task CreateAsync(Payer payer);

    Task<Payer?> GetPayerByUserIdNTAsync(string userId);

    Task<Payer?> GetPayerByStripeCustomerIdNTAsync(string stripeCustomerId);

    Task<Payer?> GetPayerByUserIdAsync(string userId);
}