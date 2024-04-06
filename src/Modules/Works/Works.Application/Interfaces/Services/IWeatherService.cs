namespace Works.Application.Interfaces.Services;

internal interface IWeatherService
{
    Task<WeatherResponse> GetActualWeatherAsync(string cityName);

    Task<WeathersResponse> GetForecastsAsync(WeathersRequest request);
}