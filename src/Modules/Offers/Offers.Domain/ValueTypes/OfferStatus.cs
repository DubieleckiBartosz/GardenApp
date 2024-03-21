namespace Offers.Domain.ValueTypes;

public class OfferStatus : Enumeration
{
    public static OfferStatus Pending = new OfferStatus(1, nameof(Pending));
    public static OfferStatus Accepted = new OfferStatus(2, nameof(Accepted));
    public static OfferStatus Rejected = new OfferStatus(3, nameof(Rejected));
    public static OfferStatus Expired = new OfferStatus(4, nameof(Expired));
    public static OfferStatus Canceled = new OfferStatus(5, nameof(Canceled));

    protected OfferStatus(short id, string name) : base(id, name)
    {
    }
}