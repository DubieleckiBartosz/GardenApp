namespace Works.Infrastructure.Configurations;

public static class WorksInfrastructureConfigurations
{
    public static WebApplicationBuilder RegisterWorksInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();

        builder
            .WorksDatabaseConfiguration()
            .WorksDependencyInjection()
            .RegisterWeatherClient();

        return builder;
    }

    private static WebApplicationBuilder WorksDependencyInjection(this WebApplicationBuilder builder)
    {
        //REPOSITORIES
        builder.Services
            .AddScoped<IGardeningWorkRepository, GardeningWorkRepository>()
            .AddScoped<IWorkItemRepository, WorkItemRepository>()
            .AddScoped<IGardeningWorkRepositoryDao, GardeningWorkRepositoryDao>();

        return builder;
    }

    private static WebApplicationBuilder WorksDatabaseConfiguration(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var options = builder.Configuration.GetSection("EfOptions").Get<EfOptions>()!;
        var schema = WorksContext.WorksSchema;
        var connectionString = options.ConnectionString + schema;

        builder.RegisterEntityFrameworkNpg<WorksContext>(connectionString, schema);

        return builder;
    }

    private static WebApplicationBuilder RegisterWeatherClient(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<CachedWeatherHandler>()
            .AddSingleton<IWeatherClient, WeatherClient>();

        builder.Services.AddHttpClient("weather", _ =>
        {
            _.Timeout = TimeSpan.FromSeconds(5);
        })
          .AddPolicyHandler(Policy
          .HandleResult<HttpResponseMessage>(r =>
                !r.IsSuccessStatusCode).RetryAsync(3, (de, cnt, c) =>
                {
                    Log.Error($"RetryCount: {cnt}, result = {de.Result.StatusCode}. Date-{Clock.CurrentDate()}");
                }));

        return builder;
    }
}