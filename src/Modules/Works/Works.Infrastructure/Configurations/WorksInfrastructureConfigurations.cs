namespace Works.Infrastructure.Configurations;

public static class WorksInfrastructureConfigurations
{
    public static WebApplicationBuilder RegisterWorksInfrastructure(this WebApplicationBuilder builder)
    {
        builder.WorksDatabaseConfiguration().WorksDependencyInjection();
        return builder;
    }

    private static WebApplicationBuilder WorksDependencyInjection(this WebApplicationBuilder builder)
    {
        //REPOSITORIES
        builder.Services
            .AddScoped<IGardeningWorkRepository, GardeningWorkRepository>()
            .AddScoped<IWorkItemRepository, WorkItemRepository>();

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
}