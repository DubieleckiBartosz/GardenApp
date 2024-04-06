namespace Works.Application.Models.Weather.Forecast;

public class ForecastResponse
{
    public string Cod { get; set; }
    public int Message { get; set; }
    public int Cnt { get; set; }
    public List<ForecastWeather> List { get; set; }
    public ForecastCity City { get; set; }
}