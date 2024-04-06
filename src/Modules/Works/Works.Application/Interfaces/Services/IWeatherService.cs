namespace Works.Application.Interfaces.Services;

public interface IWeatherService
{
    Task<ActualResponse> GetActualWeatherAsync(string cityName);

    Task<ForecastResponse> GetForecastsAsync(ForecastRequest request);

    Task<GeoLocation> GetLocationByCityNameAsync(string cityName);

    Task<HistoryResponse> GetHistoryAsync(HistoryRequest request);
}