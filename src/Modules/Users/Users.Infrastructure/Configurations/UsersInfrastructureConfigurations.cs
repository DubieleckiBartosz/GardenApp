using BuildingBlocks.Application.Options;
using BuildingBlocks.Infrastructure.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.Database;
using Users.Infrastructure.Integration;

namespace Users.Infrastructure.Configurations;

internal static class UsersInfrastructureConfigurations
{
    public static WebApplicationBuilder GetUsersInfrastructureConfigurations(this WebApplicationBuilder builder)
    {
        builder.UsersDatabaseConfiguration();

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

        services.AddSingleton<EventNavigator>();

        return builder;
    }

    private static WebApplicationBuilder RegisterNavigators(this WebApplicationBuilder builder)
    {
        var sp = builder.Services.BuildServiceProvider();

        using var scope = sp.CreateScope();

        var eventBus = scope.ServiceProvider.GetRequiredService < c EventNavigator > ();

        EventNavigator.AddNavigation<TestIntegrationEvent>(_ => )

        return builder;
    }
}