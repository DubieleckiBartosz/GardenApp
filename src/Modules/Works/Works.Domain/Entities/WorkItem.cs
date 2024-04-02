namespace Works.Domain.Entities;

public class WorkItem
{
    private List<TimeWeatherRecord> _timeWeatherRecords;

    public string Name { get; private set; }
    public IEnumerable<TimeWeatherRecord> TimeWeatherRecords => _timeWeatherRecords;

    private WorkItem()
    {
        _timeWeatherRecords = new List<TimeWeatherRecord>();
    }
}