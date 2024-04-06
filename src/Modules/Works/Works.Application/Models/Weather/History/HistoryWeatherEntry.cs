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
}