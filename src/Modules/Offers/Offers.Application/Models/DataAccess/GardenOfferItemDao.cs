namespace Offers.Application.Models.DataAccess;

public class GardenOfferItemDao
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}