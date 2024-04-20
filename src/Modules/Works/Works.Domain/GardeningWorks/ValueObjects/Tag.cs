namespace Works.Domain.GardeningWorks.ValueObjects;

public class Tag : ValueObject
{
    public string Value { get; }
    public string Bg { get; }
    public string Text { get; }

    public Tag(string value, string bg, string text)
    {
        Value = value;
        Bg = bg;
        Text = text;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
        yield return Bg;
        yield return Text;
    }

    public static explicit operator Tag(string tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
        {
            throw new ArgumentException("Invalid string format", nameof(tag));
        }

        var values = tag.Split('|');
        if (values.Length != 3)
        {
            throw new ArgumentException("String must contain exactly 3 values separated by '|'", nameof(tag));
        }

        return new Tag(values[0], values[1], values[2]);
    }

    public override string ToString() => $"{Value}|{Bg}|{Text}";
}