namespace BuildingBlocks.Infrastructure.Outbox;

public interface IOutboxAccessor
{
    Task AddAsync(OutboxMessage message);

    Task<IEnumerable<Guid>> GetUnprocessedMessageIdsAsync();

    Task SetMessageToProcessedAsync(Guid id);

    Task DeleteAsync(IEnumerable<OutboxMessage> messages);

    Task<OutboxMessage?> GetMessageAsync(Guid id);
}