using BuildingBlocks.Application.Models.Outbox;

namespace BuildingBlocks.Application.Contracts.Outbox;

public interface IOutboxStore
{
    Task AddAsync(OutboxMessage message);

    Task<IEnumerable<Guid>> GetUnprocessedMessageIdsAsync();

    Task SetMessageToProcessedAsync(Guid id);

    void Delete(IEnumerable<OutboxMessage> messages);

    Task<OutboxMessage?> GetMessageAsync(Guid id);
}