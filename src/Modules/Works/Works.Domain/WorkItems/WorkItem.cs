namespace Works.Domain.WorkItems;

public class WorkItem : Entity, IAggregateRoot
{
    public string BusinessId { get; }
    public int GardeningWorkId { get; }
    public string Name { get; private set; }
    public DateTime? EstimatedStartTime { get; private set; }
    public DateTime? EstimatedEndTime { get; private set; }
    public int? RealTimeInMinutes { get; private set; }
    public WorkItemStatus Status { get; private set; }
    public List<TimeWeatherRecord> TimeWeatherRecords { get; private set; }

    private WorkItem()
    {
        TimeWeatherRecords = new List<TimeWeatherRecord>();
    }

    private WorkItem(
        int gardeningWorkId,
        string businessId,
        string name,
        DateTime? estimatedStartTime,
        DateTime? estimatedEndTime)
    {
        BusinessId = businessId;
        GardeningWorkId = gardeningWorkId;
        Name = name;
        EstimatedStartTime = estimatedStartTime;
        EstimatedEndTime = estimatedEndTime;
        TimeWeatherRecords = new List<TimeWeatherRecord>();
        Status = WorkItemStatus.OnHold;
        IncrementVersion();
    }

    internal static WorkItem Create(int gardeningWorkId,
                                    string businessId,
                                    string name,
                                    DateTime? estimatedStartTime,
                                    DateTime? estimatedEndTime)
    {
        return new WorkItem(gardeningWorkId, businessId, name, estimatedStartTime, estimatedEndTime);
    }

    public TimeWeatherRecord AddTimeWeatherRecord(TimeLog timeLog, List<Weather> weathers)
    {
        var newTimeWeatherRecord = new TimeWeatherRecord(timeLog, weathers);

        TimeWeatherRecords.Add(newTimeWeatherRecord);

        RealTimeInMinutes += timeLog.Minutes;
        IncrementVersion();

        return newTimeWeatherRecord;
    }

    public void UpdateTimeWeatherRecord(int timeWeatherRecordId, TimeLog timeLog, List<Weather> weathers)
    {
        var record = TimeWeatherRecords.FirstOrDefault(_ => _.Id == timeWeatherRecordId);
        if (record == null)
        {
            throw new RecordNotFoundException(timeWeatherRecordId);
        }

        record.Update(timeLog, weathers);
        IncrementVersion();
    }

    public void UpdateStatus(WorkItemStatus itemStatus)
    {
        Status = itemStatus;
        IncrementVersion();
    }
}