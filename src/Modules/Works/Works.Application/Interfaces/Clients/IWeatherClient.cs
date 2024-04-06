namespace Works.Application.Interfaces.Clients;

public interface IWeatherClient
{
    Task<string?> GetWeathersAsync(string countryCode, string cityName);

    Task<string?> GetActualWeatherDataAsync(string cityName);
}