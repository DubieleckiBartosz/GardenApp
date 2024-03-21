namespace Offers.Infrastructure.Configurations;

public static class InfrastructureConfigurations
{
    public static WebApplicationBuilder GetInfrastructureConfigurations(this WebApplicationBuilder builder)
    {
        builder.GetInfrastructureDependencyInjection().GetDatabaseConfiguration();
        return builder;
    }

    private static WebApplicationBuilder GetInfrastructureDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IGardenOfferRepositoryDao, GardenOfferRepositoryDao>()
            .AddScoped<IGardenOfferRepository, GardenOfferRepository>();

        return builder;
    }

    private static WebApplicationBuilder GetDatabaseConfiguration(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var options = builder.Configuration.GetSection("EfOptions").Get<EfOptions>()!;
        var schema = OffersContext.OffersSchema;
        var connectionString = options.ConnectionString + schema;

        builder.RegisterEntityFrameworkNpg<OffersContext>(connectionString, schema);

        return builder;
    }
}