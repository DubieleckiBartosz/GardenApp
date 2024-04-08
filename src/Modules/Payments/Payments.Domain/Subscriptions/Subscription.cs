using Payments.Domain.Subscriptions.ValueTypes;

namespace Payments.Domain.Subscriptions;

internal class Subscription : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public SubType Type { get; }
    public decimal Price { get; }
    public string Currency { get; }
    public string StripePriceId { get; }
    public DateTime Created { get; }

    public Subscription(string name, SubType type, decimal price, string currency, string stripePriceId)
    {
        Name = name;
        Type = type;
        Price = price;
        Currency = currency;
        StripePriceId = stripePriceId;
        Created = Clock.CurrentDate();
    }
}