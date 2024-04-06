namespace Works.Application.Models;

internal class Weather
{
    [JsonProperty("description")]
    public string Description { get; init; }
}