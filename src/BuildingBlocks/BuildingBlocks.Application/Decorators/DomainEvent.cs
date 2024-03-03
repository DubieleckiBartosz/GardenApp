namespace BuildingBlocks.Application.Decorators;

public class DomainEvent<T> : INotification where T : IDomainEvent
{
    public T Event { get; }

    public DomainEvent(T @event)
    {
        this.Event = @event;
    }
}