namespace BuildingBlocks.Application.Contracts.Integration;

public interface IEventDispatcher
{
    Task SendAsync(IEvent @event);

    Task PublishAsync(CancellationToken cancellationToken = default, params IEvent[] events);
}