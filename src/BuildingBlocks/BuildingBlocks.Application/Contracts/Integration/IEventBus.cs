namespace BuildingBlocks.Application.Contracts.Integration;

public interface IEventBus
{
    Task Publish<T>(T @event)
           where T : IEvent;

    void Subscribe<T>(IIntegrationEventHandler<T> handler)
        where T : IEvent;
}