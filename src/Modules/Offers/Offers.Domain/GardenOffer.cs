namespace Offers.Domain;

public class GardenOffer : Entity, IAggregateRoot
{
    private readonly List<GardenOfferItem> _offerItems;
    public string CreatorId { get; }
    public string CreatorName { get; }
    public string Description { get; private set; }
    public decimal TotalPrice { get; private set; }
    public string Recipient { get; private set; }
    public Date ExpirationDate { get; private set; }
    public OfferStatus Status { get; private set; }
    public bool IsExpired => ExpirationDate.Value <= Clock.CurrentDate();

    private GardenOffer()
    { }

    private GardenOffer(
        string creatorId,
        string creatorName,
        string recipient,
        string name,
        string description,
        decimal price,
        Date expirationDate)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (description == null)
        {
            throw new ArgumentNullException(nameof(description));
        }

        Description = description;
        TotalPrice = price;
        CreatorId = creatorId;
        CreatorName = creatorName;
        Recipient = recipient;
        Status = OfferStatus.Pending;
        ExpirationDate = expirationDate;
        _offerItems = new();
    }

    public static GardenOffer NewGardenOffer(
        string creatorId,
        string creatorName,
        string recipient,
        string name,
        string description,
        decimal price,
        Date expirationDate)
        => new(creatorId, creatorName, recipient, name, description, price, expirationDate);
}