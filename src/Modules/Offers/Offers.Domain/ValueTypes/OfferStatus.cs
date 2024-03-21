namespace Offers.Domain.ValueTypes;

public class OfferStatus : Enumeration
{
    //After creating
    public static OfferStatus Pending = new OfferStatus(1, nameof(Pending));

    //After completing the offer
    public static OfferStatus Completed = new OfferStatus(2, nameof(Completed));

    //Accepted by client
    public static OfferStatus Accepted = new OfferStatus(3, nameof(Accepted));

    //Rejected by client
    public static OfferStatus Rejected = new OfferStatus(4, nameof(Rejected));

    //Expire after 7 days
    public static OfferStatus Expired = new OfferStatus(5, nameof(Expired));

    //Rejected by creator
    public static OfferStatus Canceled = new OfferStatus(6, nameof(Canceled));

    protected OfferStatus(short id, string name) : base(id, name)
    {
    }
}