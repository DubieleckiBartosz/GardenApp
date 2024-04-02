namespace Works.Domain.ValueObjects;

public class TimeWeatherRecord : ValueObject
{
    public int Minutes { get; }
    public DateTime Date { get; }
    public DateTime Created { get; }
    public Weather Weather { get; }

    private TimeWeatherRecord()
    {
    }

    public TimeWeatherRecord(Weather weather, int minutes, DateTime date, DateTime created)
    {
        Minutes = minutes;
        Date = date;
        Created = created;
        Weather = weather ?? throw new ArgumentNullException(nameof(weather));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Minutes;
        yield return Date;
        yield return Created;
        yield return Weather;
    }
}