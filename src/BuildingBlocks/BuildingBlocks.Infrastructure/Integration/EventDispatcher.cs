using BuildingBlocks.Application.Contracts.Outbox;
using BuildingBlocks.Application.Models.Outbox;
using BuildingBlocks.Application.Settings;
using Newtonsoft.Json;

namespace BuildingBlocks.Infrastructure.Integration;

internal class EventDispatcher : IEventDispatcher
{
    private readonly IOutboxListener _outboxListener;

    public EventDispatcher(IOutboxListener outboxListener)
    {
        _outboxListener = outboxListener;
    }

    public async Task SendAsync(IDomainEvent @event)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        var type = @event.GetType().AssemblyQualifiedName;
        var data = JsonConvert.SerializeObject(@event, JsonSettings.DefaultSerializerSettings);
        var outboxMessage = new OutboxMessage(type!, data);

        await _outboxListener.AddAsync(outboxMessage);
    }
}