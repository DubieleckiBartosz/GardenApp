namespace BuildingBlocks.Domain.ValueObjects;

public class Phone : ValueObject
{
    public string Value { get; }

    public Phone(string? value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("PhoneNumber");
        }

        var regex = new Regex(@"^[0-9]{3}-[0-9]{3}-[0-9]{3}$");
        var match = regex.Match(value);
        if (!match.Success)
        {
            throw new InvalidPhoneNumberException(value);
        }

        Value = value;
    }

    public static implicit operator Phone(string? value) => new Phone(value);

    public static implicit operator string(Phone value) => value.Value;

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return this.Value;
    }
}