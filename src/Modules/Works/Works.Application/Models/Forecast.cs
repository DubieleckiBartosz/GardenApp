namespace Works.Application.Models;

internal class Forecast
{
    [JsonProperty("weather")]
    public Weather[] Weather { get; init; }

    [JsonProperty("main")]
    public Main Main { get; init; }

    [JsonProperty("clouds")]
    public Clouds Clouds { get; init; }

    [JsonProperty("wind")]
    public Wind Wind { get; init; }

    [JsonProperty("dt_txt")]
    public string Dt_txt { get; init; }

    [JsonProperty("dt")]
    public long Dt { get; init; }
}