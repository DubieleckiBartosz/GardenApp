namespace BuildingBlocks.Application.Decorators;

public class MediatorDecorator : IDomainDecorator
{
    private readonly IMediator _mediator;

    public MediatorDecorator(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task Publish<TNotification>(TNotification notification,
        CancellationToken cancellationToken = default(CancellationToken)) where TNotification : IDomainEvent
    {
        var wrapper = this.CreateNotification(notification);
        await this._mediator.Publish(wrapper, cancellationToken);
    }

    private INotification CreateNotification(IDomainEvent domainEvent)
    {
        if (domainEvent == null)
        {
            throw new ArgumentNullException(nameof(domainEvent));
        }

        var genericDispatcherType = typeof(DomainEvent<>).MakeGenericType(domainEvent.GetType());
        return (INotification)Activator.CreateInstance(genericDispatcherType, domainEvent)!;
    }
}