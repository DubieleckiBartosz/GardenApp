namespace Works.Application.Models.DataAccess;

public class WeatherDao
{
    public int Clouds { get; set; }
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
    public decimal Wind { get; set; }

    public WeatherDao(int clouds, DateTime date, int temperatureC, string summary, decimal wind)
    {
        Clouds = clouds;
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
        Wind = wind;
    }

    public static explicit operator WeatherDao(string weather)
    {
        if (string.IsNullOrWhiteSpace(weather))
        {
            throw new ArgumentException("Invalid string format", nameof(weather));
        }

        var values = weather.Split('|');

        return new WeatherDao(
                int.Parse(values[0]),
                DateTime.Parse(values[1]),
                int.Parse(values[2]),
                values[3],
                decimal.Parse(values[4]));
    }
}