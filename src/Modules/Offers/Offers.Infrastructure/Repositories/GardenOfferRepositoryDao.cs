namespace Offers.Infrastructure.Repositories;

internal class GardenOfferRepositoryDao : BaseRepository, IGardenOfferRepositoryDao
{
    public GardenOfferRepositoryDao(IConfiguration configuration, ILogger logger)
        : base(configuration["DapperConnectionString"]!, logger)
    {
    }

    public async Task<IEnumerable<GardenOfferItemDao>?> GetGardenOfferItemsBySearchAsync(
        string creatorId,
        string? recipient,
        int? status,
        decimal? totalPrice,
        string sortModelType,
        string sortModelName,
        int pageNumber,
        int pageSize)
    {
        var param = new DynamicParameters();

        param.Add("@creatorId", creatorId);
        param.Add("@totalPrice", totalPrice);
        param.Add("@recipient", recipient);
        param.Add("@status", status);
        param.Add("@sortModelType", sortModelType);
        param.Add("@sortModelName", sortModelName);
        param.Add("@pageNumber", pageNumber);
        param.Add("@pageSize", pageSize);

        var result = await QueryAsync<GardenOfferItemDao>(
            "SELECT offers.gardenOffer_getBySearch_S(" +
            "@creatorId, @totalPrice, @recipient, @status, @sortModelType, @sortModelName, @pageNumber, @pageSize)", param);

        return result;
    }
}