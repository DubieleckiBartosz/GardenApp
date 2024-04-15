namespace Works.Domain.GardeningWorks.ValueObjects;

internal class Tag : ValueObject
{
    public string Value { get; }

    public string Color { get; }

    public Tag(string value, string color)
    {
        Value = value;
        Color = color;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
        yield return Color;
    }

    public static explicit operator Tag(string tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
        {
            throw new ArgumentException("Invalid string format", nameof(tag));
        }

        var values = tag.Split('|');
        if (values.Length != 2)
        {
            throw new ArgumentException("String must contain exactly 2 values separated by '|'", nameof(tag));
        }

        return new Tag(values[0], values[1]);
    }

    public override string ToString() => $"{Value}|{Color}";
}