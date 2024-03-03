namespace BuildingBlocks.Domain.Abstractions;

public interface IDomainDecorator
{
    Task Publish<TNotification>(TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification : IDomainEvent;
}