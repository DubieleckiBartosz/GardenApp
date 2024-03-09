namespace Panels.Infrastructure.Integration;

public class IntegrationEventHandler<T> : IIntegrationEventHandler<T>
        where T : IntegrationEvent
{
    private readonly IInboxAccessor _inboxAccessor;

    public IntegrationEventHandler(IInboxAccessor inboxAccessor)
    {
        _inboxAccessor = inboxAccessor;
    }

    public async Task Handle(T @event)
    {
        var data = JsonConvert.SerializeObject(@event, JsonSettings.DefaultSerializerSettings);

        var type = @event.GetType().FullName!;
        var newInboxMessage = new InboxMessage(@event.Id, @event.OccurredOn, type, data);
        try
        {
            await _inboxAccessor.AddAsync(newInboxMessage);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}