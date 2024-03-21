namespace Offers.Application.Models.DataAccess;

public class GardenOfferDao
{
    public string CreatorName { get; set; } = default!;
    public string CreatorId { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Recipient { get; set; } = default!;
    public decimal TotalPrice { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Status { get; set; }
    public IEnumerable<GardenOfferItemDao>? OfferItems { get; set; }
}