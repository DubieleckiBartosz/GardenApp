namespace Works.Domain.ValueObjects;

public class Location : ValueObject
{
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}