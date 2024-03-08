namespace BuildingBlocks.Application.Contracts.Integration;

public interface IEventBus
{
    Task Publish<T>(T @event)
           where T : IntegrationEvent;

    void Subscribe<T>(Func<T, Task> handler)
        where T : IntegrationEvent;
}