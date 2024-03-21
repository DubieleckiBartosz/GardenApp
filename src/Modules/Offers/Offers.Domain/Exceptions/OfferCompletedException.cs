namespace Offers.Domain.Exceptions;

internal class OfferCompletedException : BaseException
{
    public OfferCompletedException(int offerId) : base($"The offer with the status 'completed' is immutable. [OfferId: {offerId}]")
    {
    }
}