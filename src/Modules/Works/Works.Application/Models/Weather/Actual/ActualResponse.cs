using Works.Application.Models.Weather.Forecast;

namespace Works.Application.Models.Weather.Actual;

public class ActualResponse
{
    [JsonProperty("coord")]
    public Coord Coord { get; set; }

    [JsonProperty("weather")]
    public List<ActualWeather> Weather { get; set; }

    [JsonProperty("base")]
    public string Base { get; set; }

    [JsonProperty("main")]
    public ActualMain Main { get; set; }

    [JsonProperty("visibility")]
    public int Visibility { get; set; }

    [JsonProperty("wind")]
    public ActualWind Wind { get; set; }

    [JsonProperty("clouds")]
    public Clouds Clouds { get; set; }

    [JsonProperty("dt")]
    public long Dt { get; set; }

    [JsonProperty("sys")]
    public ActualSys Sys { get; set; }

    [JsonProperty("timezone")]
    public int Timezone { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("cod")]
    public int Cod { get; set; }
}