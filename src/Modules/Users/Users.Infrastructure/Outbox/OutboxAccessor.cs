namespace Users.Infrastructure.Outbox;

internal class OutboxAccessor : IOutbox
{
    public Task AddAsync(OutboxMessage message)
    {
        throw new NotImplementedException();
    }
}