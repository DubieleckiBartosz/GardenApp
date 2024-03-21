namespace Offers.Infrastructure.Repositories;

internal class GardenOfferRepository : IGardenOfferRepository
{
    private readonly DbSet<GardenOffer> _gardenOffers;
    private readonly OffersContext _offersContext;

    public IUnitOfWork UnitOfWork => _offersContext;

    public GardenOfferRepository(OffersContext offersContext)
    {
        _gardenOffers = offersContext.GardenOffers;
        _offersContext = offersContext;
    }

    public async Task AddAsync(GardenOffer gardenOffer)
    {
        await _gardenOffers.AddAsync(gardenOffer);
    }

    public async Task<GardenOffer?> GetGardenOfferByRecipientAndStatusAsync(string recipient, OfferStatus offerStatus) =>
        await _gardenOffers.FirstOrDefaultAsync(_ => _.Recipient == recipient && _.Status == offerStatus);

    public async Task<GardenOffer?> GetGardenOfferWithItemsByIdAsync(int offerId) =>
        await _gardenOffers.IncludePaths(GardenOfferEntityTypeConfiguration.OfferItems)
        .FirstOrDefaultAsync(_ => _.Id == offerId);
}