namespace Works.Application.Handlers.WorkItem.Parameters;

public class AddTimeWeatherRecordParameters
{
    public int WorkItemId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}