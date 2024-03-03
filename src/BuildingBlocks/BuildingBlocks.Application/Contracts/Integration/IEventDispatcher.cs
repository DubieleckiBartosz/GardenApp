namespace BuildingBlocks.Application.Contracts.Integration;

public interface IEventDispatcher
{
    Task PublishAsync(params IEvent[] @events);
}