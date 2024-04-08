namespace Payments.Domain.SubPayments.ValueTypes;

public class SubPaymentStatus : Enumeration
{
    public static SubPaymentStatus Active = new SubPaymentStatus(1, nameof(Active));

    public static SubPaymentStatus Suspended = new SubPaymentStatus(2, nameof(Suspended));

    public static SubPaymentStatus Canceled = new SubPaymentStatus(3, nameof(Canceled));

    protected SubPaymentStatus(short id, string name) : base(id, name)
    {
    }
}