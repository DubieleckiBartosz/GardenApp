namespace Works.Domain.GardeningWorks;

public class GardeningWork : Entity, IAggregateRoot
{
    private string _tags;
    public string BusinessId { get; }
    public Email ClientEmail { get; }
    public GardeningWorkPriority Priority { get; private set; }
    public FutureDate PlannedStartDate { get; private set; }
    public DateTime? RealStartDate { get; private set; }
    public FutureDate? PlannedEndDate { get; private set; }
    public DateTime? RealEndDate { get; private set; }
    public Location Location { get; private set; }
    public GardeningWorkStatus Status { get; private set; }

    private GardeningWork()
    {
    }

    private GardeningWork(
        string businessId,
        Email clientEmail,
        FutureDate plannedStartDate,
        DateTime? realStartDate,
        DateTime? plannedEndDate,
        DateTime? realEndDate,
        Location location)
    {
        ClientEmail = clientEmail;
        PlannedStartDate = plannedStartDate;
        RealStartDate = realStartDate;
        PlannedEndDate = plannedEndDate;
        RealEndDate = realEndDate;
        Location = location;
        Status = GardeningWorkStatus.OnHold;
        BusinessId = businessId;
        IncrementVersion();
    }

    public static GardeningWork Create(
        string businessId,
        string clientEmail,
        DateTime plannedStartDate,
        DateTime? realStartDate,
        DateTime? plannedEndDate,
        DateTime? realEndDate,
        Location location)
    {
        return new GardeningWork(
            businessId,
            clientEmail,
            plannedStartDate,
            realStartDate,
            plannedEndDate,
            realEndDate,
            location);
    }

    public WorkItem NewWorkItem(string name, DateTime? estimatedStartTime = null, DateTime? estimatedEndTime = null)
    {
        if (Status == GardeningWorkStatus.Canceled || Status == GardeningWorkStatus.Close)
        {
            throw new BadStatusException(Status, this.Id);
        }

        return WorkItem.Create(this.Id, this.BusinessId, name, estimatedStartTime, estimatedEndTime);
    }

    public void UpdateStatus(GardeningWorkStatus gardeningWorkStatus)
    {
        Status = gardeningWorkStatus;
        IncrementVersion();
    }

    public void UpdatePlannedStartDate(FutureDate startDate)
    {
        PlannedStartDate = startDate;
        IncrementVersion();
    }

    public void UpdatePlannedEndDate(FutureDate endDate)
    {
        PlannedEndDate = endDate;
        IncrementVersion();
    }

    public void SetAsCLose(DateTime realEndDate)
    {
        if (RealStartDate.HasValue && realEndDate > RealStartDate.Value)
        {
            throw new InvalidEndDateException(this.Id);
        }

        RealEndDate = realEndDate;
        Status = GardeningWorkStatus.Close;
        IncrementVersion();
    }
}