namespace Works.Application.Handlers.WorkItem.Parameters;

public class UpdateTimeWeatherRecordParameters
{
    public int WorkItemId { get; init; }
    public int TimeWeatherRecordId { get; init; }
    public int Minutes { get; init; }
    public DateTime Date { get; init; }

    public UpdateTimeWeatherRecordParameters()
    {
    }

    [JsonConstructor]
    public UpdateTimeWeatherRecordParameters(
        int workItemId,
        int timeWeatherRecordId,
        int minutes,
        DateTime date)
    {
        WorkItemId = workItemId;
        TimeWeatherRecordId = timeWeatherRecordId;
        Minutes = minutes;
        Date = date;
    }
}