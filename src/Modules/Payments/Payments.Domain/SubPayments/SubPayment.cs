namespace Payments.Domain.SubPayments;

public class SubPayment : Entity, IAggregateRoot
{
    public int PayerId { get; }
    public string CustomerSubscriptionId { get; }
    public SubPaymentStatus Status { get; private set; }
    public DateTime StartDate { get; }
    public DateTime CurrentPeriodEnd { get; private set; }

    internal SubPayment()
    { }

    internal SubPayment(
        int payerId,
        string customerSubscriptionId,
        DateTime currentPeriodEnd)
    {
        PayerId = payerId;
        CustomerSubscriptionId = customerSubscriptionId;
        Status = SubPaymentStatus.Active;
        StartDate = Clock.CurrentDate();
        CurrentPeriodEnd = currentPeriodEnd;
    }
}