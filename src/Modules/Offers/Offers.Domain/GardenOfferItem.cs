namespace Offers.Domain;

public class GardenOfferItem : Entity
{
    public int Id { get; }
    public string Code { get; }
    public string Name { get; }
    public decimal Price { get; private set; }

    private GardenOfferItem()
    {
    }

    private GardenOfferItem(
        string code,
        string name,
        decimal price)
    {
        if (code == null)
        {
            throw new ArgumentNullException(nameof(code));
        }

        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        Code = code;
        Name = name.ToUpper();
        Price = price;
        Version++;
    }

    public static GardenOfferItem NewGardenOfferItem(
        string creatorId, string name, decimal price)
        => new(creatorId, name, price);
}