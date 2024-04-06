using Works.Application.Interfaces.Services;
using Works.Application.Models.Weather.Forecast;

namespace GardenApp.API.Modules;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public TestController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetActualWeatherAsync([FromQuery] string cityName)
    {
        var response = await _weatherService.GetActualWeatherAsync(cityName);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GetForecastsAsync([FromQuery] ForecastRequest request)
    {
        var response = await _weatherService.GetForecastsAsync(request);
        return Ok(response);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetLocationByCityNameAsync([FromQuery] string cityName)
    {
        var response = await _weatherService.GetLocationByCityNameAsync(cityName);
        return Ok(response);
    }
}