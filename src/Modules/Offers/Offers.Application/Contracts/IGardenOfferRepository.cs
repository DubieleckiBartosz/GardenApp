using Offers.Domain.ValueTypes;

namespace Offers.Application.Contracts;

public interface IGardenOfferRepository : IRepository<GardenOffer>
{
    Task AddAsync(GardenOffer gardenOfferItem);

    Task<GardenOffer?> GetGardenOfferByRecipientAndStatusAsync(string recipient, OfferStatus offerStatus);

    Task<GardenOffer?> GetGardenOfferWithItemsByIdAsync(int offerId);
}