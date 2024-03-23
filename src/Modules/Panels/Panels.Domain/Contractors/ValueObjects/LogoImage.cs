namespace Panels.Domain.Contractors.ValueObjects;

public class LogoImage : ValueObject
{
    public string Key { get; }

    public LogoImage(string key)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        Key = key;
    }

    public static implicit operator LogoImage(string key) => new(key);

    public static implicit operator string(LogoImage logo) => logo.Key;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Key;
    }
}