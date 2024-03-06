namespace BuildingBlocks.Application.Contracts.Integration;

public interface IEventNavigator
{
    IntegrationEvent Map(IDomainEvent @event);
}