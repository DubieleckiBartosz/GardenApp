namespace BuildingBlocks.Infrastructure.Integration;

internal class EventBus : IEventBus
{
    private readonly IRabbitEventListener _rabbitEventListener;
    private readonly IEventDispatcher _eventDispatcher;

    public EventBus(IRabbitEventListener rabbitEventListener, IEventDispatcher eventDispatcher)
    {
        _rabbitEventListener = rabbitEventListener;
        _eventDispatcher = eventDispatcher;
    }

    public async Task CommitAsync(params IEvent[] events)
    {
        foreach (var @event in events)
        {
            await _rabbitEventListener.PublishAsync(@event);
        }
    }

    public async Task PublishLocalAsync(params IEvent[] events)
    {
        await _eventDispatcher.PublishAsync(events);
    }
}