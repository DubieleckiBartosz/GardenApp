namespace Works.Application.Handlers.GardeningWork.Views;

public class WorkItemViewModel
{
    public int WorkItemId { get; }
    public string Name { get; }
    public DateTime? EstimatedStartTime { get; }
    public DateTime? EstimatedEndTime { get; }
    public int? RealTimeInMinutes { get; }
    public int Status { get; }
    public List<TimeWeatherRecordViewModel>? TimeWeatherRecords { get; }

    private WorkItemViewModel(
        int workItemId,
        string name,
        DateTime? estimatedStartTime,
        DateTime? estimatedEndTime,
        int? realTimeInMinutes,
        int status,
        List<TimeWeatherRecordViewModel>? timeWeatherRecords)
    {
        WorkItemId = workItemId;
        Name = name;
        EstimatedStartTime = estimatedStartTime;
        EstimatedEndTime = estimatedEndTime;
        RealTimeInMinutes = realTimeInMinutes;
        Status = status;
        TimeWeatherRecords = timeWeatherRecords;
    }

    public static implicit operator WorkItemViewModel(WorkItemDao workItemDao)
    {
        var records = workItemDao.TimeWeatherRecords?.Select(_ => (TimeWeatherRecordViewModel)_).ToList();

        return new(workItemDao.Id, workItemDao.Name, workItemDao.EstimatedStartTime, workItemDao.EstimatedEndTime, workItemDao.RealTimeInMinutes, workItemDao.Status, records);
    }
}