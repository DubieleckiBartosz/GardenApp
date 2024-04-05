namespace Works.Application.Handlers.WorkItem.Parameters;

public class WorkItemUpdateStatusParameters
{
    public int WorkItemId { get; init; }
    public int Status { get; init; }

    public WorkItemUpdateStatusParameters()
    { }

    [JsonConstructor]
    public WorkItemUpdateStatusParameters(int workItemId, int status)
    {
        WorkItemId = workItemId;
        Status = status;
    }
}