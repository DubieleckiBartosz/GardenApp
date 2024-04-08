namespace Payments.Domain.Sessions.ValueTypes;

public class PaymentStatus : Enumeration
{
    public static PaymentStatus New = new PaymentStatus(1, nameof(New));

    public static PaymentStatus Success = new PaymentStatus(2, nameof(Success));

    public static PaymentStatus Canceled = new PaymentStatus(3, nameof(Canceled));

    protected PaymentStatus(short id, string name) : base(id, name)
    {
    }
}