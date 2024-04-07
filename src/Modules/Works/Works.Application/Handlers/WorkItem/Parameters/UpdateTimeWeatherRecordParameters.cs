namespace Works.Application.Handlers.WorkItem.Parameters;

public class UpdateTimeWeatherRecordParameters
{
    public int WorkItemId { get; init; }
    public int TimeWeatherRecordId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    public UpdateTimeWeatherRecordParameters()
    {
    }

    [JsonConstructor]
    public UpdateTimeWeatherRecordParameters(
        int workItemId,
        int timeWeatherRecordId,
        DateTime startDate,
        DateTime endDate)
    {
        WorkItemId = workItemId;
        TimeWeatherRecordId = timeWeatherRecordId;
        StartDate = startDate;
        EndDate = endDate;
    }
}