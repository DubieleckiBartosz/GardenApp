namespace Works.Application.Models.Weather.History;

public class HistoryRain
{
    [JsonProperty("1h")]
    public double OneHour { get; set; }
}