namespace Payments.Domain.SubPayments.ValueTypes;

public class SubPaymentStatus : Enumeration
{
    public static SubPaymentStatus Active = new SubPaymentStatus(1, nameof(Active));

    public static SubPaymentStatus PendingCancellation = new SubPaymentStatus(2, nameof(PendingCancellation));

    public static SubPaymentStatus Canceled = new SubPaymentStatus(3, nameof(Canceled));

    protected SubPaymentStatus(short id, string name) : base(id, name)
    {
    }
}