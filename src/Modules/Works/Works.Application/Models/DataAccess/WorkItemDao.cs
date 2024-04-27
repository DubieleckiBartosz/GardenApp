namespace Works.Application.Models.DataAccess;

public class WorkItemDao
{
    public int Id { get; set; }
    public int GardeningWorkId { get; }
    public string Name { get; set; }
    public DateTime? EstimatedStartTime { get; set; }
    public DateTime? EstimatedEndTime { get; set; }
    public int? RealTimeInMinutes { get; set; }
    public int Status { get; set; }
    public IEnumerable<TimeWeatherRecordDao> TimeWeatherRecords { get; set; }
}