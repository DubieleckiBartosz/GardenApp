namespace Works.Domain.WorkItems.ValueObjects;

public class Weather : ValueObject
{
    public int Clouds { get; }
    public string Date { get; }
    public int TemperatureC { get; }

    public string Summary { get; }
    public decimal Wind { get; }

    public int TemperatureF =>
        32 + (int)(TemperatureC / 0.5556);

    private Weather()
    { }

    public Weather(
        int clouds,
        string date,
        int temperatureC,
        string summary,
        decimal wind)
    {
        Clouds = clouds;
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
        Wind = wind;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Clouds;
        yield return Date;
        yield return TemperatureC;
        yield return Summary;
        yield return Wind;
    }
}