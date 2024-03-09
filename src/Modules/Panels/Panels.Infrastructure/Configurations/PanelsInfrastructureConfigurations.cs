namespace Panels.Infrastructure.Configurations;

public static class PanelsInfrastructureConfigurations
{
    public static WebApplicationBuilder RegisterPanelsInfrastructure(this WebApplicationBuilder builder)
    {
        builder.PanelsDatabaseConfiguration().PanelsDependencyInjection();
        return builder;
    }

    private static WebApplicationBuilder PanelsDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IInboxAccessor, InboxAccessor>();

        //HOSTED service
        builder.Services.AddHostedService<InboxProcess>();

        return builder;
    }

    private static WebApplicationBuilder PanelsDatabaseConfiguration(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var options = builder.Configuration.GetSection("EfOptions").Get<EfOptions>()!;
        var schema = PanelsContext.PanelsSchema;
        var connectionString = options.ConnectionString + schema;

        builder.RegisterEntityFrameworkNpg<PanelsContext>(connectionString, schema);

        return builder;
    }

    public static WebApplication InitializePanelsEvents(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IEventRegistry>();
        typeof(PanelsAppAssemblyReference).Assembly.RegistrationAssemblyIntegrationEvents(service);

        return app;
    }

    public static WebApplication SubscribePanelsIntegrationEvents(this WebApplication app)
    {
        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        var eventBus = app.Services.GetRequiredService<IEventBus>();

        IntegrationEventSubscriber<TestIntegrationEvent>(eventBus, scopeFactory, Log.Logger);
        return app;
    }

    private static void IntegrationEventSubscriber<T>(IEventBus eventBus, IServiceScopeFactory scopeFactory, ILogger logger)
            where T : IntegrationEvent
    {
        logger.Information($"Subscribe to {typeof(T).FullName}");

        eventBus.Subscribe<T>(async (@event) =>
        {
            using var scope = scopeFactory.CreateScope();
            var accessor = scope.ServiceProvider.GetRequiredService<IInboxAccessor>();
            await new IntegrationEventHandler<T>(accessor).Handle(@event);
        });
    }
}