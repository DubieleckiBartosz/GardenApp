namespace Works.Infrastructure.Clients;

internal class WeatherClient : IWeatherClient
{
    private readonly string _weatherKey;
    private record Error(string Message);
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger _logger;

    public WeatherClient(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ILogger logger)
    {
        _weatherKey = configuration["WeatherClient:WeatherApiKey"]!;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<string?> GetWeathersAsync(string countryCode, string cityName)
    {
        var httpClient = _httpClientFactory.CreateClient("weather");
        var request = new HttpRequestMessage(HttpMethod.Get, $"data/2.5/forecast?q={cityName},{countryCode}&appid={_weatherKey}&units=metric");

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
        var request = new HttpRequestMessage(HttpMethod.Get, $"data/2.5/weather?q={cityName}&appid={_weatherKey}&units=metric");

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
            $"Phrase: {response.ReasonPhrase}, code: {(int)response.StatusCode}, message: {resultError?.Message ?? "empty"}.");
    }
}