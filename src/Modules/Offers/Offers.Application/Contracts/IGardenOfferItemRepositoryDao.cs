using Offers.Application.Models.DataAccess;

namespace Offers.Application.Contracts;

public interface IGardenOfferItemRepositoryDao
{
    Task<IEnumerable<GardenOfferItemDao>?> GetGardenOfferItemsBySearchAsync(
        string creatorId, string? code, string? name, decimal? price,
        string sortModelType, string sortModelName, int pageNumber, int pageSize);
}