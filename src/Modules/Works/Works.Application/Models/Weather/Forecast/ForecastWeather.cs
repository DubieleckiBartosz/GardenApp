namespace Works.Application.Models.Weather.Forecast;

public class ForecastWeather
{
    [JsonProperty("dt")]
    public long Dt { get; set; }

    [JsonProperty("main")]
    public ForecastMain Main { get; set; }

    [JsonProperty("weather")]
    public List<ForecastWeatherInfo> Weather { get; set; }

    [JsonProperty("clouds")]
    public Clouds Clouds { get; set; }

    [JsonProperty("wind")]
    public ForecastWind Wind { get; set; }

    [JsonProperty("visibility")]
    public int Visibility { get; set; }

    [JsonProperty("pop")]
    public double Pop { get; set; }

    [JsonProperty("sys")]
    public ForecastSys Sys { get; set; }

    [JsonProperty("dt_txt")]
    public string DtTxt { get; set; }
}