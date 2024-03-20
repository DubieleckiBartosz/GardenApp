namespace Offers.Infrastructure.Configurations;

public static class InfrastructureConfigurations
{
    public static WebApplicationBuilder GetInfrastructureConfigurations(this WebApplicationBuilder builder)
    {
        builder.GetInfrastructureDependencyInjection();
        return builder;
    }

    private static WebApplicationBuilder GetInfrastructureDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IGardenOfferItemRepositoryDao, GardenOfferItemRepositoryDao>()
            .AddScoped<IGardenOfferItemRepository, GardenOfferItemRepository>();

        return builder;
    }
}