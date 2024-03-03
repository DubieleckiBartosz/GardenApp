namespace BuildingBlocks.Application.Dispatchers;

public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync(params IEvent[] events)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var scopedProcessingService =
                scope.ServiceProvider.GetRequiredService<IMediator>();
            foreach (var @event in @events)
            {
                await scopedProcessingService.Publish(@event);
            }
        }
    }
}