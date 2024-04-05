namespace Works.Application.Handlers.GardeningWork.Parameters;

public class UpdatePlannedStartDateParameters
{
    public int GardeningWorkId { get; init; }
    public DateTime NewPlannedStartDate { get; init; }

    public UpdatePlannedStartDateParameters()
    { }

    [JsonConstructor]
    public UpdatePlannedStartDateParameters(int gardeningWorkId, DateTime newPlannedStartDate)
    {
        GardeningWorkId = gardeningWorkId;
        NewPlannedStartDate = newPlannedStartDate;
    }
}