namespace Works.Application.Models.Weather;

public class Coord
{
    [JsonProperty("lat")]
    public double Lat { get; set; }

    [JsonProperty("lon")]
    public double Lon { get; set; }
}