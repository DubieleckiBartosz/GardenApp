namespace Works.Application.Handlers.GardeningWork.Parameters;

public class GardeningWorkUpdateStatusParameters
{
    public int GardeningWorkId { get; init; }
    public int NewStatus { get; init; }

    public GardeningWorkUpdateStatusParameters()
    { }

    [JsonConstructor]
    public GardeningWorkUpdateStatusParameters(int gardeningWorkId, int newStatus)
    {
        GardeningWorkId = gardeningWorkId;
        NewStatus = newStatus;
    }
}