namespace Works.Application.Models;

internal class City
{
    [JsonProperty("id")]
    public int Id { get; init; }

    [JsonProperty("name")]
    public string Name { get; init; }

    [JsonProperty("country")]
    public string Country { get; init; }
}