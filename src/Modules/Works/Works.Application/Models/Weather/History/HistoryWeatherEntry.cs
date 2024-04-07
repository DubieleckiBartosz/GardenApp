namespace Works.Application.Models.Weather.History;

public class HistoryWeatherEntry
{
    [JsonProperty("dt")]
    public long Dt { get; set; }

    [JsonProperty("main")]
    public HistoryMain Main { get; set; }

    [JsonProperty("wind")]
    public HistoryWind Wind { get; set; }

    [JsonProperty("clouds")]
    public Clouds Clouds { get; set; }

    [JsonProperty("weather")]
    public List<HistoryWeather> Weather { get; set; }

    [JsonProperty("rain")]
    public HistoryRain Rain { get; set; }

    internal Domain.WorkItems.ValueObjects.Weather Map()
    {
        var date = DateTimeOffset.FromUnixTimeSeconds(Dt).DateTime.ToUTC();
        var tempCelsius = (int)Math.Round(Convert.ToDecimal(Main.Temp) - 273.15m);
        var summary = Weather.Count > 0 ? $"{Weather[0].Main}: {Weather[0].Description}" : WeatherMessage.NotAvailable;
        var wind = Convert.ToDecimal(Wind.Speed);

        return new(Clouds.All, date, tempCelsius, summary, wind);
    }
}