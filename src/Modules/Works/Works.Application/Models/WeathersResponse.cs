namespace Works.Application.Models;

internal class WeathersResponse
{
    [JsonProperty("city")]
    public City City { get; init; }

    [JsonProperty("list")]
    public Forecast[] List { get; init; }
}