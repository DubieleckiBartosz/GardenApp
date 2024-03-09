namespace Users.Infrastructure.Configurations;

public static class UsersInfrastructureConfigurations
{
    public static WebApplicationBuilder GetUsersInfrastructureConfigurations(this WebApplicationBuilder builder)
    {
        builder.UsersDatabaseConfiguration().RegisterDependencyInjection();

        return builder;
    }

    private static WebApplicationBuilder UsersDatabaseConfiguration(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var options = builder.Configuration.GetSection("EfOptions").Get<EfOptions>()!;
        var schema = UsersContext.UsersSchema;
        var connectionString = options.ConnectionString + schema;

        builder.RegisterEntityFrameworkNpg<UsersContext>(connectionString, schema);

        return builder;
    }

    private static WebApplicationBuilder RegisterDependencyInjection(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddScoped<IOutboxAccessor, OutboxAccessor>();

        //HOSTED service
        services.AddHostedService<OutboxProcessor>();

        return builder;
    }
}