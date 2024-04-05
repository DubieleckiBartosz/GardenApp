﻿namespace Works.Domain.WorkItems;

public class WorkItem : Entity, IAggregateRoot
{
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

    private WorkItem(int gardeningWorkId, string name, int? estimatedTimeInMinutes)
    {
        GardeningWorkId = gardeningWorkId;
        Name = name;
        EstimatedTimeInMinutes = estimatedTimeInMinutes;
        TimeWeatherRecords = new List<TimeWeatherRecord>();
        Status = WorkItemStatus.OnHold;
    }

    internal static WorkItem Create(int gardeningWorkId, string name, int? estimatedTimeInMinutes)
    {
        return new WorkItem(gardeningWorkId, name, estimatedTimeInMinutes);
    }

    public void AddTimeWeatherRecord(TimeLog timeLog, Weather weather)
    {
        var newTimeWeatherRecord = new TimeWeatherRecord(timeLog, weather);

        TimeWeatherRecords.Add(newTimeWeatherRecord);

        RealTimeInMinutes += timeLog.Minutes;
    }

    public void UpdateTimeWeatherRecord(int timeWeatherRecordId, int minutes, DateTime date)
    {
        var record = TimeWeatherRecords.FirstOrDefault(_ => _.Id == timeWeatherRecordId);
        if (record == null)
        {
            throw new RecordNotFoundException(timeWeatherRecordId);
        }

        var timeLog = new TimeLog(minutes, date);
        record.UpdateTime(timeLog);
    }

    public void UpdateStatus(WorkItemStatus itemStatus) => Status = itemStatus;
}