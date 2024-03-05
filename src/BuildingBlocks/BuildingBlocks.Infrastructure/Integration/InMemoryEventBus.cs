namespace BuildingBlocks.Infrastructure.Integration;

internal class InMemoryEventBus
{
    static InMemoryEventBus()
    {
    }

    private InMemoryEventBus()
    {
        _handlersDictionary = new Dictionary<string, List<IIntegrationEventHandler>>();
    }

    public static InMemoryEventBus Instance { get; } = new InMemoryEventBus();

    private readonly Dictionary<string, List<IIntegrationEventHandler>> _handlersDictionary;

    public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
    {
        var eventType = typeof(T).FullName;
        if (eventType != null)
        {
            if (_handlersDictionary.ContainsKey(eventType))
            {
                var handlers = _handlersDictionary[eventType];
                handlers.Add(handler);
            }
            else
            {
                _handlersDictionary.Add(eventType, new List<IIntegrationEventHandler>() { handler });
            }
        }
    }

    public async Task Publish<T>(T @event)
        where T : IntegrationEvent
    {
        var eventType = @event.GetType().FullName;

        if (eventType == null)
        {
            return;
        }

        var integrationEventHandlers = _handlersDictionary[eventType];

        foreach (var integrationEventHandler in integrationEventHandlers)
        {
            if (integrationEventHandler is IIntegrationEventHandler<T> handler)
            {
                await handler.Handle(@event);
            }
        }
    }
}