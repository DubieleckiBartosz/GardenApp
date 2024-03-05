namespace BuildingBlocks.Infrastructure.Integration;

public class EventBus : IEventBus
{
    private readonly ILogger _logger;

    public EventBus(ILogger logger)
    {
        _logger = logger;
    }

    public async Task Publish<T>(T @event)
        where T : IntegrationEvent
    {
        _logger.Information("Publishing {Event}", @event.GetType().FullName);
        await InMemoryEventBus.Instance.Publish(@event);
    }

    public void Subscribe<T>(IIntegrationEventHandler<T> handler)
        where T : IntegrationEvent
    {
        InMemoryEventBus.Instance.Subscribe(handler);
    }
}