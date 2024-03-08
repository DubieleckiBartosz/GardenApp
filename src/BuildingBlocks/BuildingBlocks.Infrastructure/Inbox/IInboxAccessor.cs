namespace BuildingBlocks.Infrastructure.Inbox;

public interface IInboxAccessor
{
    Task AddAsync(InboxMessage message);

    Task<IEnumerable<Guid>> GetUnprocessedMessageIdsAsync();

    Task SetMessageToProcessedAsync(Guid id);

    Task DeleteAsync(IEnumerable<InboxMessage> messages);

    Task<InboxMessage?> GetMessageAsync(Guid id);
}