namespace Works.Domain.WorkItems;

public class WorkItem : Entity, IAggregateRoot
{
    public string BusinessId { get; }
    public int GardeningWorkId { get; }
    public string Name { get; private set; }
    public int? EstimatedTimeInMinutes { get; private set; }
    public int? RealTimeInMinutes { get; private set; }
    public WorkItemStatus Status { get; private set; }
    public List<TimeWeatherRecord> TimeWeatherRecords { get; private set; }

    private WorkItem()
    {
        TimeWeatherRecords = new List<TimeWeatherRecord>();
    }

    private WorkItem(int gardeningWorkId, string businessId, string name, int? estimatedTimeInMinutes)
    {
        BusinessId = businessId;
        GardeningWorkId = gardeningWorkId;
        Name = name;
        EstimatedTimeInMinutes = estimatedTimeInMinutes;
        TimeWeatherRecords = new List<TimeWeatherRecord>();
        Status = WorkItemStatus.OnHold;
        Version++;
    }

    internal static WorkItem Create(int gardeningWorkId, string businessId, string name, int? estimatedTimeInMinutes)
    {
        return new WorkItem(gardeningWorkId, businessId, name, estimatedTimeInMinutes);
    }

    public TimeWeatherRecord AddTimeWeatherRecord(TimeLog timeLog, List<Weather> weathers)
    {
        var newTimeWeatherRecord = new TimeWeatherRecord(timeLog, weathers);

        TimeWeatherRecords.Add(newTimeWeatherRecord);

        RealTimeInMinutes += timeLog.Minutes;
        Version++;

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
        Version++;
    }

    public void UpdateStatus(WorkItemStatus itemStatus)
    {
        Status = itemStatus;
        Version++;
    }
}