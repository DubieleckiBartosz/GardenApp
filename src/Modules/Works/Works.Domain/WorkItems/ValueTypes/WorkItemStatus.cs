namespace Works.Domain.WorkItems.ValueTypes;

public class WorkItemStatus : Enumeration
{
    public static WorkItemStatus OnHold = new WorkItemStatus(1, nameof(OnHold));
    public static WorkItemStatus InProgress = new WorkItemStatus(2, nameof(InProgress));
    public static WorkItemStatus Completed = new WorkItemStatus(3, nameof(Completed));
    public static WorkItemStatus Canceled = new WorkItemStatus(4, nameof(Canceled));

    protected WorkItemStatus(short id, string name) : base(id, name)
    {
    }
}