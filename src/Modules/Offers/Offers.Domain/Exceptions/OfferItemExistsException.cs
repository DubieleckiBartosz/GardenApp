namespace Offers.Domain.Exceptions;

internal class OfferItemExistsException : BaseException
{
    public OfferItemExistsException(int offerId, string itemName) : base($"The offer already includes this item [OfferId: {offerId}, itemName: {itemName}]")
    {
    }
}