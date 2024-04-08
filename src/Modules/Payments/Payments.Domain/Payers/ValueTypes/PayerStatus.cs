namespace Payments.Domain.Payers.ValueTypes;

public class PayerStatus : Enumeration
{
    public static PayerStatus Active = new PayerStatus(1, nameof(Active));

    public static PayerStatus Inactive = new PayerStatus(2, nameof(Inactive));

    protected PayerStatus(short id, string name) : base(id, name)
    {
    }
}