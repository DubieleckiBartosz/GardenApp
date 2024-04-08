namespace Payments.Domain.Subscriptions.ValueTypes;

public class SubType : Enumeration
{
    public static SubType Free = new SubType(1, nameof(Free));

    public static SubType Standard = new SubType(2, nameof(Standard));

    public static SubType Premium = new SubType(3, nameof(Premium));

    protected SubType(short id, string name) : base(id, name)
    {
    }
}