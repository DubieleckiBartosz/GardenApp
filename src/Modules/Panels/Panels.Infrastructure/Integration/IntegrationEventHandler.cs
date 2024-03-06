namespace Panels.Infrastructure.Integration;

internal class IntegrationEventHandler<T> : IIntegrationEventHandler<T>
        where T : IntegrationEvent
{
    public Task Handle(T @event)
    {
        //Insert to inbox
        throw new NotImplementedException();
    }
}