namespace BuildingBlocks.Application.Decorators;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<DomainEvent<TDomainEvent>> where TDomainEvent : IDomainEvent
{
}