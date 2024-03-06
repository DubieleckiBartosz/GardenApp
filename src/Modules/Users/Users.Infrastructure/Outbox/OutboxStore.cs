using BuildingBlocks.Domain.Time;
using Users.Infrastructure.Database;

namespace Users.Infrastructure.Outbox;

internal sealed class OutboxStore : IOutboxStore
{
    private readonly DbSet<OutboxMessage> _outboxMessages;

    public OutboxStore(UsersContext usersContext)
    {
        _outboxMessages = usersContext.OutboxMessages;
    }

    async Task IOutboxStore.AddAsync(OutboxMessage message)
    {
        await _outboxMessages.AddAsync(message);
    }

    void IOutboxStore.Delete(IEnumerable<OutboxMessage> messages) => _outboxMessages.RemoveRange(messages);

    async Task<OutboxMessage?> IOutboxStore.GetMessageAsync(Guid id) => await _outboxMessages.FirstOrDefaultAsync(message => message.Id == id);

    async Task<IEnumerable<Guid>> IOutboxStore.GetUnprocessedMessageIdsAsync() => await _outboxMessages.Where(_ => _.ProcessedDate == null).Select(message => message.Id).ToListAsync();

    async Task IOutboxStore.SetMessageToProcessedAsync(Guid id)
    {
        await _outboxMessages
            .Where(_ => _.Id == id)
            .ExecuteUpdateAsync(_ => _.SetProperty(x => x.ProcessedDate, Clock.CurrentDate()));
    }
}