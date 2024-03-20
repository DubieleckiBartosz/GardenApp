using Dapper;
using Offers.Application.Models.DataAccess;

namespace Offers.Infrastructure.Repositories;

internal class GardenOfferItemRepositoryDao : BaseRepository, IGardenOfferItemRepositoryDao
{
    public GardenOfferItemRepositoryDao(string dbConnection, ILogger logger) : base(dbConnection, logger)
    {
    }

    public async Task<IEnumerable<GardenOfferItemDao>?> GetGardenOfferItemsBySearchAsync(
        string creatorId,
        string? code,
        string? name,
        decimal? price,
        string sortModelType,
        string sortModelName,
        int pageNumber,
        int pageSize)
    {
        var param = new DynamicParameters();

        param.Add("@creatorId", creatorId);
        param.Add("@price", price);
        param.Add("@code", code);
        param.Add("@name", name);
        param.Add("@sortModelType", sortModelType);
        param.Add("@sortModelName", sortModelName);
        param.Add("@pageNumber", pageNumber);
        param.Add("@pageSize", pageSize);

        var result = await QueryAsync<GardenOfferItemDao>(
            "SELECT offers.gardenOfferItem_getBySearch_S(" +
            "@creatorId, @price, @code, @name, @sortModelType, @sortModelName, @pageNumber, @pageSize)", param);

        return result;
    }
}