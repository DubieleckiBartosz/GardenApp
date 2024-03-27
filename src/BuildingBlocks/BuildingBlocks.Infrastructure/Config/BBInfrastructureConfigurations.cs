namespace BuildingBlocks.Infrastructure.Config;

public static class BBInfrastructureConfigurations
{
    public static WebApplicationBuilder RegisterBBInfrastructureConfigurations(this WebApplicationBuilder builder)
    {
        builder.RegisterDependencyInjection().AddMinio();

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
            .AddScoped<IEventDispatcher, EventDispatcher>()
            .AddSingleton<IEventRegistry, EventRegistry>()
            .AddSingleton<IEventBus, EventBus>();

        //MODULE CLIENT
        builder.Services
            .AddSingleton<IModuleClient, ModuleClient>()
            .AddSingleton<IModuleSubscriber, ModuleSubscriber>()
            .AddSingleton<IModuleActionRegistration, ModuleActionRegistration>();

        //FILE STOREAGE
        builder.Services.AddScoped<IFileStorage, FileStorage.FileStorage>();

        return builder;
    }

    public static WebApplicationBuilder AddMinio(this WebApplicationBuilder builder)
    {
        if (builder.Configuration.GetSection("MinioOptions:IsActive").Get<bool>())
        {
            builder.Services.Configure<MinioOptions>(builder.Configuration.GetSection("MinioOptions"));
            builder.Services.TryAddSingleton(sp => sp.GetRequiredService<IMinioFactory>().CreateClient());
            builder.Services.AddSingleton<IMinioService, MinioService>();
        }

        return builder;
    }

    public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
    => app.ApplicationServices.GetRequiredService<IModuleSubscriber>();
}