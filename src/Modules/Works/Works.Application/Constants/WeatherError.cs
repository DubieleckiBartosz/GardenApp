namespace Works.Application.Constants;

internal static class WeatherError
{
    public const string RequestIsNull = "Weather request is null!";
    public const string RequestDataNull = "City and country are required!";
    public const string CityIsNull = "City is required!";

    public static string CountryCodeNotFound(string code) => $"Code not found in the garden application. [Code: {code}]";

    public static string WeatherNotFound(string code, string cityName) => $"The weather client response is null. [Code: {code}, City name: {cityName}]";

    public static string CityNotFound(string cityName) => $"The weather client response is null. [City name: {cityName}]";

    public static string LocationNotFound(string cityName) => $"Location not found. [City name: {cityName}]";

    public static string HistoryNotFound(double lat, double lon, long start, long end) => $"History not found. [Lat: {lat}, Lon: {lon}, Start: {start}, End: {end}]";
}