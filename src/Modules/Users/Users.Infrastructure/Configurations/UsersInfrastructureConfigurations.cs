﻿using Users.Infrastructure.Repositories;

namespace Users.Infrastructure.Configurations;

public static class UsersInfrastructureConfigurations
{
    public static WebApplicationBuilder GetUsersInfrastructureConfigurations(this WebApplicationBuilder builder)
    {
        builder.RegisterIdentity().RegisterDependencyInjection();

        return builder;
    }

    private static WebApplicationBuilder RegisterDependencyInjection(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services
            .AddScoped<IOutboxAccessor, OutboxAccessor>()
            .AddScoped<IUserRepository, UserRepository>();

        //HOSTED service
        services.AddHostedService<OutboxProcessor>();

        return builder;
    }
}