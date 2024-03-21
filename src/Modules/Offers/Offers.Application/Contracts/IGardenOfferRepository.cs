using Offers.Domain.ValueTypes;

namespace Offers.Application.Contracts;

public interface IGardenOfferRepository : IRepository<GardenOffer>
{
    Task<GardenOffer?> GetGardenOfferByIdAsync(int offerId);

    Task AddAsync(GardenOffer gardenOfferItem);

    Task<GardenOffer?> GetGardenOfferByRecipientAndStatusNTAsync(string recipient, OfferStatus offerStatus);

    Task<GardenOffer?> GetGardenOfferWithItemsByIdAsync(int offerId);
}