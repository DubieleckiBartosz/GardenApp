namespace Offers.Infrastructure.Repositories;

internal class GardenOfferItemRepositoryDao : BaseRepository, IGardenOfferItemRepositoryDao
{
    public GardenOfferItemRepositoryDao(string dbConnection, ILogger logger) : base(dbConnection, logger)
    {
    }
}