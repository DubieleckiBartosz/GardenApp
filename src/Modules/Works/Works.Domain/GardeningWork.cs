namespace Works.Domain;

public class GardeningWork : Entity, IAggregateRoot
{
    public IEnumerable<WorkItem> _workItems;
    public string ClientEmail { get; }
    public DateTime PlannedStartDate { get; private set; }
    public DateTime? RealStartDate { get; private set; }
    public DateTime? PlannedEndDate { get; private set; }
    public DateTime? RealEndDate { get; private set; }
    public int? EstimatedTimeInMinutes { get; private set; }
    public int? RealTimeInMinutes { get; private set; }
    public Location Location { get; }
    public GardeningWorkStatus Status { get; private set; }

    public IEnumerable<WorkItem> WorkItems => _workItems;

    private GardeningWork()
    {
        _workItems = new List<WorkItem>();
    }
}