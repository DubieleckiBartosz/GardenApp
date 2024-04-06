namespace Works.Application.Models.Weather.History;

public class HistoryRequest
{
    public double Lat { get; init; }
    public double Lon { get; init; }

    //Start date (unix time, UTC time zone), e.g. start=1369728000
    public long StartDate { get; init; }

    // (unix time, UTC time zone), e.g. end=1369789200
    public long EndDate { get; init; }
}