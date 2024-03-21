namespace Offers.Application.Contracts;

public interface IGardenOfferRepository : IRepository<GardenOffer>
{
    Task AddAsync(GardenOffer gardenOfferItem);

    Task<GardenOffer?> GetGardenOfferByRecipientAndStatusNTAsync(string recipient, OfferStatus offerStatus);

    Task<GardenOffer?> GetGardenOfferWithItemsByIdAsync(int offerId);
}