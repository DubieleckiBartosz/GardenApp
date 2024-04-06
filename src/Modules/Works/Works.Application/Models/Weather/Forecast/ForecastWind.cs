namespace Works.Application.Models.Weather.Forecast;

public class ForecastWind
{
    [JsonProperty("speed")]
    public double Speed { get; set; }

    [JsonProperty("deg")]
    public int Deg { get; set; }

    [JsonProperty("gust")]
    public double Gust { get; set; }
}