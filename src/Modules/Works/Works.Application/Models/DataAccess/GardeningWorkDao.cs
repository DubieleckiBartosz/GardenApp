namespace Works.Application.Models.DataAccess;

public class GardeningWorkDao
{
    public int Id { get; }
    public string ClientEmail { get; }
    public int Priority { get; }
    public DateTime PlannedStartDate { get; }
    public int Status { get; }
    public string City { get; }
    public string Street { get; }
    public string NumberStreet { get; }
    public string BusinessId { get; }
}