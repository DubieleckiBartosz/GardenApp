namespace Works.Application.Config;

public static class WorksApplicationConfigurations
{
    public static WebApplicationBuilder RegisterWorksApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IWeatherService, WeatherService>();
        return builder;
    }
}