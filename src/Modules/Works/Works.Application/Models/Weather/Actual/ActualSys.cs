namespace Works.Application.Models.Weather.Actual;

public class ActualSys
{
    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("sunrise")]
    public long Sunrise { get; set; }

    [JsonProperty("sunset")]
    public long Sunset { get; set; }
}