namespace Payments.Domain.Sessions;

public class PaymentSession : Entity, IAggregateRoot
{
    public int PayerId { get; }
    public string SessionId { get; }
    public string SubscriptionId { get; }
    public PaymentStatus Status { get; private set; }
    public DateTime Created { get; }
    public DateTime Modified { get; private set; }

    private PaymentSession()
    { }

    public PaymentSession(
        int payerId,
        string sessionId,
        string subscriptionId)
    {
        PayerId = payerId;
        SessionId = sessionId;
        SubscriptionId = subscriptionId;
        Status = PaymentStatus.New;
        Created = Clock.CurrentDate();
        Modified = Clock.CurrentDate();
        IncrementVersion();
    }

    public void SetStatus(PaymentStatus paymentStatus)
    {
        Status = paymentStatus;
        Modified = Clock.CurrentDate();
        IncrementVersion();
    }
}