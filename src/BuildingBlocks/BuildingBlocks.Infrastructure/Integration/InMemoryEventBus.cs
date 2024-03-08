namespace BuildingBlocks.Infrastructure.Integration;

public sealed class InMemoryEventBus
{
    static InMemoryEventBus()
    {
    }

    private InMemoryEventBus()
    {
        _handlersDictionary = new Dictionary<string, List<Func<IntegrationEvent, Task>>>();
    }

    public static InMemoryEventBus Instance { get; } = new InMemoryEventBus();

    private readonly Dictionary<string, List<Func<IntegrationEvent, Task>>> _handlersDictionary;

    public void Subscribe<T>(Func<T, Task> handler) where T : IntegrationEvent
    {
        var eventType = typeof(T).Name;
        if (eventType != null)
        {
            Func<IntegrationEvent, Task> wrappedHandler = (integrationEvent) => handler((T)integrationEvent);

            if (_handlersDictionary.ContainsKey(eventType))
            {
                var handlers = _handlersDictionary[eventType];
                handlers.Add(wrappedHandler);
            }
            else
            {
                _handlersDictionary.Add(eventType, new() { wrappedHandler });
            }
        }
    }

    public async Task Publish<T>(T @event)
        where T : IntegrationEvent
    {
        var eventType = @event.GetType().Name;

        if (eventType == null)
        {
            return;
        }

        var integrationEventHandlers = _handlersDictionary[eventType];

        foreach (var integrationEventHandler in integrationEventHandlers)
        {
            await integrationEventHandler.Invoke(@event);
        }
    }
}