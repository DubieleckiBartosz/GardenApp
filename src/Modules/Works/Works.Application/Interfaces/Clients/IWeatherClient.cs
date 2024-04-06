namespace Works.Application.Interfaces.Clients;

public interface IWeatherClient
{
    Task<string?> GetForecastAsync(string countryCode, string cityName);

    Task<string?> GetActualWeatherDataAsync(string cityName);

    Task<string?> GetHistoryWeatherDataAsync(double lat, double lon, long start, long end);

    Task<string?> GetLocationByCityAsync(string cityName);
}