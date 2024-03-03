namespace BuildingBlocks.Domain.ValueObjects;

public class Date
{
    public DateTime Value { get; }

    public Date(DateTime value)
    {
        Value = value.Date;
    }

    public static implicit operator DateTime(Date date)
        => date.Value;

    public static implicit operator Date(DateTime value)
        => new(value);

    public static bool operator <(Date date1, Date date2)
        => date1.Value < date2.Value;

    public static bool operator >(Date date1, Date date2)
        => date1.Value > date2.Value;

    public static bool operator <=(Date date1, Date date2)
        => date1.Value <= date2.Value;

    public static bool operator >=(Date date1, Date date2)
        => date1.Value >= date2.Value;

    public override string ToString() => Value.ToString("F");
}