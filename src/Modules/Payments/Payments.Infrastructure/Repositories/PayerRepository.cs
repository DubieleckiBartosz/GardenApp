﻿namespace Payments.Infrastructure.Repositories;

internal class PayerRepository : IPayerRepository
{
    private readonly DbSet<Payer> _payers;

    public PayerRepository(PaymentsContext paymentsContext)
    {
        _payers = paymentsContext.Payers;
    }

    public async Task CreateAsync(Payer payer)
    {
        await _payers.AddAsync(payer);
    }

    public async Task<Payer?> GetPayerByStripeCustomerIdNTAsync(string stripeCustomerId)
    {
        return await _payers.AsNoTracking().FirstOrDefaultAsync(x => x.StripeCustomerId == stripeCustomerId);
    }

    public async Task<Payer?> GetPayerByUserIdNTAsync(string userId)
    {
        return await _payers.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);
    }
}