namespace Works.Application.Models.Weather.Forecast;

public class ForecastCity
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("coord")]
    public Coord Coord { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("population")]
    public int Population { get; set; }

    [JsonProperty("timezone")]
    public int Timezone { get; set; }

    [JsonProperty("sunrise")]
    public long Sunrise { get; set; }

    [JsonProperty("sunset")]
    public long Sunset { get; set; }
}