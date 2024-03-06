namespace BuildingBlocks.Infrastructure.Config;

public static class BBInfrastructureConfigurations
{
    public static WebApplicationBuilder RegisterBBInfrastructureConfigurations(this WebApplicationBuilder builder)
    {
        builder.RegisterDependencyInjection();

        return builder;
    }

    public static IServiceCollection RegisterEntityFrameworkNpg<T>(this WebApplicationBuilder builder, string connectionString, string schema,
        Func<DbContextOptionsBuilder, DbContextOptionsBuilder>? additionalRegistrations = null, Microsoft.Extensions.Logging.ILoggerFactory? loggerFactory = null) where T : DbContext
    {
        var services = builder.Services;
        services.AddDbContext<T>(dbContextBuilder =>
        {
            dbContextBuilder.UseNpgsql(connectionString, _ => { _.MigrationsHistoryTable("__EFMigrationsHistory", schema); });
            if (loggerFactory != null)
            {
                dbContextBuilder.UseLoggerFactory(loggerFactory);
            }

            additionalRegistrations?.Invoke(dbContextBuilder);
        });

        return services;
    }

    private static WebApplicationBuilder RegisterDependencyInjection(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        services.AddScoped<IEventBus, EventBus>();

        var options = configuration.GetSection("EmailOptions").Get<EmailOptions>();
        if (options!.Enabled)
        {
            services.AddScoped<IEmailClient, LocalEmailClient>();
        }
        else
        {
            throw new NotImplementedException("Real EmailClient is not implemented.");
        }

        services
         .AddScoped<IEventBus, EventBus>()
         .AddSingleton<IEventRegistry, EventRegistry>();

        return builder;
    }
}