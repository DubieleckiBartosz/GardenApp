namespace Works.Application.Models.Weather.History;

public class HistoryWind
{
    [JsonProperty("speed")]
    public double Speed { get; set; }

    [JsonProperty("deg")]
    public int Deg { get; set; }
}