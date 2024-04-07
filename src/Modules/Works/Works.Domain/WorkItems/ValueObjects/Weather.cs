namespace Works.Domain.WorkItems.ValueObjects;

public sealed class Weather : ValueObject
{
    public int Clouds { get; }
    public string Date { get; }
    public int TemperatureC { get; }

    public string Summary { get; }
    public decimal Wind { get; }

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

    public static explicit operator Weather(string weather)
    {
        if (string.IsNullOrWhiteSpace(weather))
        {
            throw new ArgumentException("Invalid string format", nameof(weather));
        }

        var values = weather.Split('|');
        if (values.Length != 5)
        {
            throw new ArgumentException("String must contain exactly 5 values separated by '|'", nameof(weather));
        }

        return new Weather(
                int.Parse(values[0]),
                values[1],
                int.Parse(values[2]),
                values[3],
                decimal.Parse(values[4]));
    }

    public override string ToString() => $"{Clouds}|{Date}|{TemperatureC}|{Summary}|{Wind}";
}