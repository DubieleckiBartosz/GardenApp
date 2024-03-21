namespace Offers.Application.Contracts;

public interface IGardenOfferRepositoryDao
{
    Task<IEnumerable<GardenOfferItemDao>?> GetGardenOfferItemsBySearchAsync(
                string creatorId, string? recipient, int? status, decimal? totalPrice,
                string sortModelType, string sortModelName, int pageNumber, int pageSize);
}