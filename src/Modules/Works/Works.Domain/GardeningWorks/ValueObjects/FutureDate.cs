namespace Works.Domain.GardeningWorks.ValueObjects;

public class FutureDate : ValueObject
{
    public DateTime Value { get; }

    private FutureDate(DateTime value)
    {
        if (value < Clock.CurrentDate())
        {
            throw new DateMustBeFutureException(value);
        }

        Value = value.Date;
    }

    public static implicit operator DateTime(FutureDate date)
    => date.Value;

    public static implicit operator FutureDate(DateTime value)
        => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return this.Value;
    }
}