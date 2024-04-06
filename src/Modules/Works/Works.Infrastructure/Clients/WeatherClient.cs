using Serilog;
using System.Text;

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

        //var response = await httpClient.SendAsync(request);
        var jsonContent =
            "{\"message\": \"Count: 24\", \"cod\": \"200\"," +
            " \"city_id\": 4298960, \"calctime\": 0.00297316," +
            " \"cnt\": 24, \"list\": [{\"dt\": 1578384000, " +
            "\"main\": {\"temp\": 275.45, \"feels_like\": 271.7, " +
            "\"pressure\": 1014, \"humidity\": 74, \"temp_min\": 274.26," +
            " \"temp_max\": 276.48}, \"wind\": {\"speed\": 2.16, \"deg\": 87}," +
            " \"clouds\": {\"all\": 90}, " +
            "\"weather\": [{\"id\": 501, \"main\": \"Rain\", " +
            "\"description\": \"moderate rain\", \"icon\": \"10n\"}]," +
            " \"rain\": {\"1h\": 0.9}}]}";

        HttpResponseMessage response = new HttpResponseMessage();
        response.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

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