namespace Works.Application.Models;

internal class WeatherResponse
{
    [JsonProperty("main")]
    public Main Main { get; init; }

    [JsonProperty("clouds")]
    public Clouds Clouds { get; init; }

    [JsonProperty("wind")]
    public Wind Wind { get; init; }

    [JsonProperty("name")]
    public string Name { get; init; }

    [JsonProperty("weather")]
    public Weather[] Weather { get; init; }
}