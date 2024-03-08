namespace Users.Infrastructure.Outbox;

internal class OutboxAccessor : IOutboxAccessor
{
    private readonly DbSet<OutboxMessage> _outboxMessages;
    private readonly UsersContext _usersContext;

    public OutboxAccessor(UsersContext usersContext)
    {
        _outboxMessages = usersContext.OutboxMessages;
        _usersContext = usersContext;
    }

    async Task IOutboxAccessor.AddAsync(OutboxMessage message)
    {
        await _outboxMessages.AddAsync(message);
        await _usersContext.SaveChangesAsync();
    }

    async Task IOutboxAccessor.DeleteAsync(IEnumerable<OutboxMessage> messages)
    {
        _outboxMessages.RemoveRange(messages);
        await _usersContext.SaveChangesAsync();
    }

    async Task<OutboxMessage?> IOutboxAccessor.GetMessageAsync(Guid id) => await _outboxMessages.FirstOrDefaultAsync(message => message.Id == id);

    async Task<IEnumerable<Guid>> IOutboxAccessor.GetUnprocessedMessageIdsAsync() => await _outboxMessages.Where(_ => _.ProcessedDate == null).Select(message => message.Id).ToListAsync();

    async Task IOutboxAccessor.SetMessageToProcessedAsync(Guid id)
    {
        await _outboxMessages
            .Where(_ => _.Id == id)
            .ExecuteUpdateAsync(_ => _.SetProperty(x => x.ProcessedDate, Clock.CurrentDate()));

        await _usersContext.SaveChangesAsync();
    }
}