namespace Works.Application.Models.Weather.History;

public class HistoryResponse
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("cod")]
    public string Cod { get; set; }

    [JsonProperty("city_id")]
    public int CityId { get; set; }

    [JsonProperty("calctime")]
    public double CalcTime { get; set; }

    [JsonProperty("cnt")]
    public int Cnt { get; set; }

    [JsonProperty("list")]
    public List<HistoryWeatherEntry> List { get; set; }
}