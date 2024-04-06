namespace Works.Application.Models.Weather.Actual;

public class ActualWind
{
    [JsonProperty("speed")]
    public double Speed { get; set; }

    [JsonProperty("deg")]
    public int Deg { get; set; }
}