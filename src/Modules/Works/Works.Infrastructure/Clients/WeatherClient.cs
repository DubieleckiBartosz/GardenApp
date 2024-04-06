using Serilog;

namespace Works.Infrastructure.Clients;

internal class WeatherClient : IWeatherClient
{
    private readonly string _weatherKey;
    private readonly string _historyHost;
    private readonly string _apiHost;
    private record Error(int Cod, string Message);
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger _logger;

    public WeatherClient(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ILogger logger)
    {
        _weatherKey = configuration["WeatherClient:WeatherApiKey"]!;
        _historyHost = configuration["WeatherClient:HistoryHost"]!;
        _apiHost = configuration["WeatherClient:ApiHost"]!;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    //https://openweathermap.org/forecast5
    public async Task<string?> GetForecastAsync(string countryCode, string cityName)
    {
        var httpClient = _httpClientFactory.CreateClient("weather");
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"{_apiHost}/data/2.5/forecast?q={cityName},{countryCode}&appid={_weatherKey}&units=metric");

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            await ErrorProcess(response);
            return null;
        }

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string?> GetActualWeatherDataAsync(string cityName)
    {
        var httpClient = _httpClientFactory.CreateClient("weather");
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"{_apiHost}/data/2.5/weather?q={cityName}&appid={_weatherKey}&units=metric");

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            await ErrorProcess(response);
            return null;
        }

        return await response.Content.ReadAsStringAsync();
    }

    //https://openweathermap.org/history
    public async Task<string?> GetHistoryWeatherDataAsync(double lat, double lon, long start, long end)
    {
        var httpClient = _httpClientFactory.CreateClient("weather");
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"{_historyHost}/data/2.5/history/city?lat={lat}&lon={lon}&type=hour&start={start}&end={end}&appid={_weatherKey}");

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            await ErrorProcess(response);
            return null;
        }

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string?> GetLocationByCityAsync(string cityName)
    {
        var httpClient = _httpClientFactory.CreateClient("weather");
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"{_apiHost}/geo/1.0/direct?q={cityName},PL&limit=1&appid={_weatherKey}");

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            await ErrorProcess(response);
            return null;
        }

        return await response.Content.ReadAsStringAsync();
    }

    private async Task ErrorProcess(HttpResponseMessage response)
    {
        var resultError = await response.Content.ReadFromJsonAsync<Error>();
        _logger.Error(
            $"Phrase: {response.ReasonPhrase}, code: {(int)response.StatusCode}, message: {resultError?.Message}, codeMessage: {resultError?.Cod}.");
    }
}