namespace Works.Application.Services;

internal class WeatherService : IWeatherService
{
    private readonly IWeatherClient _weatherClient;

    public WeatherService(IWeatherClient weatherClient)
    {
        _weatherClient = weatherClient;
    }

    public async Task<ActualResponse> GetActualWeatherAsync(string cityName)
    {
        if (string.IsNullOrEmpty(cityName))
        {
            throw new BadRequestException(WeatherError.CityIsNull);
        }
        var response = await _weatherClient.GetActualWeatherDataAsync(cityName);
        if (response == null)
        {
            throw new NotFoundException(WeatherError.CityNotFound(cityName));
        }

        return JsonConvert.DeserializeObject<ActualResponse>(response)!;
    }

    public async Task<ForecastResponse> GetForecastsAsync(ForecastRequest request)
    {
        if (request == null)
        {
            throw new BadRequestException(WeatherError.RequestIsNull);
        }

        if (string.IsNullOrEmpty(request.CityName) || string.IsNullOrEmpty(request.CountryCode))
        {
            throw new BadRequestException(WeatherError.RequestDataNull);
        }

        var countryCode = EnumHelpers.GetEnumAttributeValueByString<CountryCodes>(request.CountryCode);

        if (countryCode == null)
        {
            throw new BadRequestException(WeatherError.CountryCodeNotFound(request.CountryCode));
        }

        var response = await _weatherClient.GetForecastAsync(request.CountryCode, request.CityName);

        if (response == null)
        {
            throw new NotFoundException(WeatherError.WeatherNotFound(request.CountryCode, request.CityName));
        }

        return JsonConvert.DeserializeObject<ForecastResponse>(response)!;
    }

    public async Task<GeoLocation> GetLocationByCityNameAsync(string cityName)
    {
        var response = await _weatherClient.GetLocationByCityAsync(cityName);

        if (response == null)
        {
            throw new NotFoundException(WeatherError.LocationNotFound(cityName));
        }

        return JsonConvert.DeserializeObject<List<GeoLocation>>(response)![0];
    }

    public async Task<HistoryResponse> GetHistoryAsync(HistoryRequest request)
    {
        if (request == null)
        {
            throw new BadRequestException(WeatherError.RequestIsNull);
        }

        var response = await _weatherClient.GetHistoryWeatherDataAsync(request.Lat, request.Lon, request.StartDate, request.EndDate);

        if (response == null)
        {
            throw new NotFoundException(WeatherError.HistoryNotFound(request.Lat, request.Lon, request.StartDate, request.EndDate));
        }

        return JsonConvert.DeserializeObject<HistoryResponse>(response)!;
    }
}