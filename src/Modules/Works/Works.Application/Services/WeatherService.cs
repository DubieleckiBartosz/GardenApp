namespace Works.Application.Services;

internal class WeatherService : IWeatherService
{
    private readonly IWeatherClient _weatherClient;

    public WeatherService(IWeatherClient weatherClient)
    {
        _weatherClient = weatherClient;
    }

    public async Task<WeatherResponse> GetActualWeatherAsync(string cityName)
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

        return JsonConvert.DeserializeObject<WeatherResponse>(response)!;
    }

    public async Task<WeathersResponse> GetForecastsAsync(WeathersRequest request)
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

        var response = await _weatherClient.GetWeathersAsync(request.CountryCode, request.CityName);

        if (response == null)
        {
            throw new NotFoundException(WeatherError.WeatherNotFound(request.CountryCode, request.CityName));
        }

        return JsonConvert.DeserializeObject<WeathersResponse>(response)!;
    }
}