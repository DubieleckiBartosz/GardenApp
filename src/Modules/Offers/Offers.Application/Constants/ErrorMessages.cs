namespace Offers.Application.Constants;

internal static class ErrorMessages
{
    internal const string SingleOfferInPendingStatus = "Only one offer can have the status 'PENDING'.";

    //Methods
    internal static string OfferNotFound(int offerId) => $"Offer not found. [OfferId: {offerId}]";
}