namespace Works.Application.Handlers.GardeningWork.Parameters;

public class UpdatePlannedEndDateParameters
{
    public int GardeningWorkId { get; init; }
    public DateTime NewPlannedEndDate { get; init; }

    public UpdatePlannedEndDateParameters()
    { }

    [JsonConstructor]
    public UpdatePlannedEndDateParameters(int gardeningWorkId, DateTime newPlannedEndDate)
    {
        GardeningWorkId = gardeningWorkId;
        NewPlannedEndDate = newPlannedEndDate;
    }
}