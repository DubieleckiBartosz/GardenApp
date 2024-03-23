namespace BuildingBlocks.Domain.ValueObjects;

public class StringValue : ValueObject
{
    public string Value { get; }

    private StringValue(string value)
    {
        var regex = new Regex(@"^[A-Za-z]+$");
        var match = regex.Match(value);
        if (!match.Success)
        {
            throw new InvalidStringException(value);
        }

        Value = value;
    }

    public static implicit operator StringValue?(string? value) => value == null ? null : new StringValue(value);

    public static implicit operator string?(StringValue? value) => value?.Value;

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return this.Value;
    }
}