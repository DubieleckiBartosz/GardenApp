namespace Works.Application.Handlers.GardeningWork.Parameters;

public class AddGardeningWorkParameters
{
    public string City { get; init; }
    public string Street { get; init; }
    public string NumberStreet { get; init; }
    public string ClientEmail { get; init; }
    public DateTime PlannedStartDate { get; init; }
    public DateTime? RealStartDate { get; init; }
    public DateTime? PlannedEndDate { get; init; }
    public DateTime? RealEndDate { get; init; }

    public AddGardeningWorkParameters()
    {
    }

    [JsonConstructor]
    public AddGardeningWorkParameters(
        string city,
        string street,
        string numberStreet,
        string clientEmail,
        DateTime plannedStartDate,
        DateTime? realStartDate,
        DateTime? plannedEndDate,
        DateTime? realEndDate)
    {
        City = city;
        Street = street;
        NumberStreet = numberStreet;
        ClientEmail = clientEmail;
        PlannedStartDate = plannedStartDate;
        RealStartDate = realStartDate;
        PlannedEndDate = plannedEndDate;
        RealEndDate = realEndDate;
    }
}