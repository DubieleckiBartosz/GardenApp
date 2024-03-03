namespace BuildingBlocks.Application.Contracts.Integration;

public interface IEventBus
{
    Task CommitAsync(params IEvent[] events);

    Task PublishLocalAsync(params IEvent[] events);
}