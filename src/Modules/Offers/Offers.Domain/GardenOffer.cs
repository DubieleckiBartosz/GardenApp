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
    {
        _offerItems = new();
    }

    private GardenOffer(
        string creatorId,
        string creatorName,
        string recipient,
        string description,
        decimal price,
        Date? expirationDate)
    {
        if (creatorName == null)
        {
            throw new ArgumentNullException(nameof(creatorName));
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
        ExpirationDate = expirationDate ?? Clock.CurrentDate().AddDays(7);
        _offerItems = new();
        IncrementVersion();
    }

    public static GardenOffer NewGardenOffer(
        string creatorId,
        string creatorName,
        string recipient,
        string description,
        decimal price,
        Date? expirationDate)
        => new(creatorId, creatorName, recipient, description, price, expirationDate);

    public void AddNewOfferItem(GardenOfferItem gardenOfferItem)
    {
        if (!OperationIsPossible)
        {
            throw new OfferCompletedException(this.Id);
        }

        var item = _offerItems.FirstOrDefault(_ => _.Name == gardenOfferItem.Name);
        if (item != null)
        {
            throw new OfferItemExistsException(Id, gardenOfferItem.Name);
        }

        _offerItems.Add(gardenOfferItem);
        IncrementVersion();
    }

    public void Complete()
    {
        if (!OperationIsPossible)
        {
            throw new NewStatusException(Status, OfferStatus.Completed);
        }

        if (!_offerItems.Any())
        {
            throw new OfferDetailsRequiredException();
        }

        Status = OfferStatus.Completed;

        this.AddEvent(new OfferCompleted(Recipient, CreatorName, TotalPrice));
    }

    private bool OperationIsPossible => Status == OfferStatus.Pending;
}