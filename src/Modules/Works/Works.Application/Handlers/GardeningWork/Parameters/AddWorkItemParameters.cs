namespace Works.Application.Handlers.GardeningWork.Parameters;

public class AddWorkItemParameters
{
    public int GardeningWorkId { get; init; }
    public string Name { get; init; }
    public int? EstimatedTimeInMinutes { get; init; }

    public AddWorkItemParameters()
    { }

    [JsonConstructor]
    public AddWorkItemParameters(string name, int? estimatedTimeInMinutes, int gardeningWorkId)
    {
        Name = name;
        EstimatedTimeInMinutes = estimatedTimeInMinutes;
        GardeningWorkId = gardeningWorkId;
    }
}