namespace Offers.Domain;

public class GardenOfferItem : Entity
{
    public string CreatorId { get; }
    public string Code { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public bool IsAvailable { get; private set; }

    private GardenOfferItem()
    {
    }

    private GardenOfferItem(
        string creatorId,
        string name,
        string description,
        decimal price,
        bool isAvailable)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (description == null)
        {
            throw new ArgumentNullException(nameof(description));
        }

        Code = CodeGenerator.GenerateUniqueCode();
        Name = name;
        Description = description;
        Price = price;
        IsAvailable = isAvailable;
        CreatorId = creatorId;
    }

    public GardenOfferItem NewGardenOfferItem(
        string creatorId, string name, string description, decimal price, bool isAvailable)
        => new(creatorId, name, description, price, isAvailable);
}