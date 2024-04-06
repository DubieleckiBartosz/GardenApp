namespace Works.Application.Models.Weather.History;

public class HistoryMain
{
    [JsonProperty("temp")]
    public double Temp { get; set; }

    [JsonProperty("feels_like")]
    public double FeelsLike { get; set; }

    [JsonProperty("pressure")]
    public int Pressure { get; set; }

    [JsonProperty("humidity")]
    public int Humidity { get; set; }

    [JsonProperty("temp_min")]
    public double TempMin { get; set; }

    [JsonProperty("temp_max")]
    public double TempMax { get; set; }
}