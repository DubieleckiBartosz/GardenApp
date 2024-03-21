namespace Offers.Application.Contracts;

public interface IGardenOfferRepository : IRepository<GardenOffer>
{
    Task AddAsync(GardenOffer gardenOfferItem);

    Task<GardenOffer?> GetGardenOfferWithItemsByIdAsync(int offerId);
}