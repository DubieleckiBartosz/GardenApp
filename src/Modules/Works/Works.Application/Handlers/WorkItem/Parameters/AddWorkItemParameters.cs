namespace Works.Application.Handlers.WorkItem.Parameters;

public class AddWorkItemParameters
{
    public int GardeningWorkId { get; init; }
    public string Name { get; init; }
    public DateTime? EstimatedStartTime { get; init; }
    public DateTime? EstimatedEndTime { get; init; }

    public AddWorkItemParameters()
    { }

    [JsonConstructor]
    public AddWorkItemParameters(
        int gardeningWorkId,
        string name,
        DateTime? estimatedStartTime,
        DateTime? estimatedEndTime)
    {
        GardeningWorkId = gardeningWorkId;
        Name = name;
        EstimatedStartTime = estimatedStartTime;
        EstimatedEndTime = estimatedEndTime;
    }
}