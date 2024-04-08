using Payments.Domain.Sessions;

namespace Payments.Domain.Payers;

public class Payer : Entity, IAggregateRoot
{
    public string UserId { get; }
    public string Email { get; }
    public string StripeCustomerId { get; }

    public Payer()
    { }

    private Payer(string userId, string email, string stripeCustomerId)
    {
        UserId = userId;
        Email = email;
        StripeCustomerId = stripeCustomerId;
    }

    public static Payer Create(string userId, string email, string stripeCustomerId)
        => new(userId, email, stripeCustomerId);

    public SubPayment CreatePaymentSubscription(string customerSubscriptionId, DateTime currentPeriodEnd)
        => new(this.Id, customerSubscriptionId, currentPeriodEnd);

    public PaymentSession CreatePaymentSession(int payerId, string sessionId, string subscriptionId)
        => new(payerId, sessionId, subscriptionId);
}