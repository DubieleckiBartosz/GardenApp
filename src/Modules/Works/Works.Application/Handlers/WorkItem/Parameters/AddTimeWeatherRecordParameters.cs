namespace Works.Application.Handlers.WorkItem.Parameters;

public class AddTimeWeatherRecordParameters
{
    public int WorkItemId { get; init; }
    public int Minutes { get; init; }
    public DateTime Date { get; init; }
}