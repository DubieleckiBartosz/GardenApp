namespace Works.Domain.WorkItems;

public class WorkItem : Entity, IAggregateRoot
{
    private List<TimeWeatherRecord> _timeWeatherRecords;
    public int GardeningWorkId { get; set; }
    public string Name { get; private set; }
    public int? EstimatedTimeInMinutes { get; private set; }
    public int? RealTimeInMinutes { get; private set; }
    public WorkItemStatus Status { get; private set; }
    public IEnumerable<TimeWeatherRecord> TimeWeatherRecords => _timeWeatherRecords;

    private WorkItem()
    {
        _timeWeatherRecords = new List<TimeWeatherRecord>();
    }

    private WorkItem(int gardeningWorkId, string name, int? estimatedTimeInMinutes)
    {
        GardeningWorkId = gardeningWorkId;
        Name = name;
        EstimatedTimeInMinutes = estimatedTimeInMinutes;
        _timeWeatherRecords = new();
        Status = WorkItemStatus.OnHold;
    }

    internal static WorkItem Create(int gardeningWorkId, string name, int? estimatedTimeInMinutes)
    {
        return new WorkItem(gardeningWorkId, name, estimatedTimeInMinutes);
    }

    public void AddTimeWeatherRecord(TimeLog timeLog, Weather weather)
    {
        var newTimeWeatherRecord = new TimeWeatherRecord(timeLog, weather);

        _timeWeatherRecords.Add(newTimeWeatherRecord);

        RealTimeInMinutes += timeLog.Minutes;
    }

    public void UpdateTimeWeatherRecord(int timeWeatherRecordId, int minutes, DateTime date)
    {
        var record = _timeWeatherRecords.FirstOrDefault(_ => _.Id == timeWeatherRecordId);
        if (record == null)
        {
            throw new RecordNotFoundException(timeWeatherRecordId);
        }

        var timeLog = new TimeLog(minutes, date);
        record.UpdateTime(timeLog);
    }

    public void UpdateStatus(WorkItemStatus itemStatus) => Status = itemStatus;
}