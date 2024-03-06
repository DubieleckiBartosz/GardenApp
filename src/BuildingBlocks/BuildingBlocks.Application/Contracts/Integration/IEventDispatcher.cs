namespace BuildingBlocks.Application.Contracts.Integration;

public interface IEventDispatcher
{
    Task SendAsync(IDomainEvent @event);
}