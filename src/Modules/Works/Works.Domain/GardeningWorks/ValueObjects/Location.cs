namespace Works.Domain.GardeningWorks.ValueObjects;

public class Location : ValueObject
{
    public string City { get; }
    public string Street { get; }
    public string NumberStreet { get; }

    private Location()
    { }

    public Location(string city, string street, string numberStreet)
        => (City, Street, NumberStreet) = (city, street, numberStreet);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return Street;
        yield return NumberStreet;
    }
}