namespace Works.Application.Models;

internal class Wind
{
    [JsonProperty("speed")]
    public decimal Speed { get; init; }
}