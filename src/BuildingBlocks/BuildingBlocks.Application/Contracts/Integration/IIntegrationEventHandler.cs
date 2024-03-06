namespace BuildingBlocks.Application.Contracts.Integration;

public interface IIntegrationEventHandler<TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}

public interface IIntegrationEventHandler
{
}