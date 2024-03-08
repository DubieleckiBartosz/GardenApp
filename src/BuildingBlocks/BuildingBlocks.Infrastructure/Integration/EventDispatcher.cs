namespace BuildingBlocks.Infrastructure.Integration;

internal class EventDispatcher : IEventDispatcher
{
    private readonly IOutboxAccessor _outboxListener;
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(
        IOutboxAccessor outboxListener,
        IServiceProvider serviceProvider)
    {
        _outboxListener = outboxListener;
        _serviceProvider = serviceProvider;
    }

    public async Task SendAsync(IEvent @event)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        var type = @event.GetType().AssemblyQualifiedName;
        var data = JsonConvert.SerializeObject(@event, JsonSettings.DefaultSerializerSettings);
        var outboxMessage = new OutboxMessage(@event.Id, @event.OccurredOn, type!, data);

        await _outboxListener.AddAsync(outboxMessage);
    }

    public async Task PublishAsync(CancellationToken cancellationToken = default, params IEvent[] events)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var scopedProcessingService =
                scope.ServiceProvider.GetRequiredService<IMediator>();
            foreach (var @event in @events)
            {
                await scopedProcessingService.Publish(@event, cancellationToken);
            }
        }
    }
}