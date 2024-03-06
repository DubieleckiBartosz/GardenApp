using BuildingBlocks.Application.Extensions;
using Panels.Infrastructure.Integration;
using Panels.Infrastructure.Integration.Events;
using Panels.Infrastructure.Reference;

namespace Panels.Infrastructure.Configurations;

public static class PanelsInfrastructureConfigurations
{
    public static WebApplicationBuilder RegisterPanelsInfrastructure(this WebApplicationBuilder builder)
    {
        builder.SubscribeIntegrationEvents();
        return builder;
    }

    public static WebApplication InitializePanelsEvents(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IEventRegistry>();
        typeof(PanelsInfrastructureAssemblyReference).Assembly.RegistrationAssemblyIntegrationEvents(service);

        return app;
    }

    public static WebApplicationBuilder SubscribeIntegrationEvents(this WebApplicationBuilder builder)
    {
        var sp = builder.Services.BuildServiceProvider();

        using var scope = sp.CreateScope();

        var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
        IntegrationEventSubscriber<TestIntegrationEvent>(eventBus, Log.Logger);

        return builder;
    }

    private static void IntegrationEventSubscriber<T>(IEventBus eventBus, ILogger logger)
            where T : IntegrationEvent
    {
        logger.Information($"Subscribe to {typeof(T).FullName}");
        eventBus.Subscribe(
            new IntegrationEventHandler<T>());
    }
}