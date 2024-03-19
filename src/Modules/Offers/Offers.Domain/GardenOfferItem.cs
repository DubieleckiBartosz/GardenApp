namespace Offers.Domain;

public class GardenOfferItem : Entity
{
    public string Code { get; }
    public string ServiceName { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public bool IsAvailable { get; private set; }

    private GardenOfferItem()
    {
    }

    private GardenOfferItem(
        string serviceName,
        string description,
        decimal price,
        bool isAvailable)
    {
        if (serviceName == null)
        {
            throw new ArgumentNullException(nameof(serviceName));
        }

        if (description == null)
        {
            throw new ArgumentNullException(nameof(description));
        }

        Code = CodeGenerator.GenerateUniqueCode();
        ServiceName = serviceName;
        Description = description;
        Price = price;
        IsAvailable = isAvailable;
    }

    public GardenOfferItem NewGardenOfferItem(string serviceName, string description, decimal price, bool isAvailable)
        => new(serviceName, description, price, isAvailable);
}