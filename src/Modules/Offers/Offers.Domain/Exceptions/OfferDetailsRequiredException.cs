namespace Offers.Domain.Exceptions;

internal class OfferDetailsRequiredException : BaseException
{
    public OfferDetailsRequiredException() : base("The offer must include some items.")
    {
    }
}