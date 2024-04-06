namespace Works.Application.Models.Geographic;

public class GeoLocation
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("local_names")]
    public Dictionary<string, string> LocalNames { get; set; }

    [JsonProperty("lat")]
    public double Latitude { get; set; }

    [JsonProperty("lon")]
    public double Longitude { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }
}