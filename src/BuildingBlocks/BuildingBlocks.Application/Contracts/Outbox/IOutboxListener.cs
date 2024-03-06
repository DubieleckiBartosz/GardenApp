using BuildingBlocks.Application.Models.Outbox;

namespace BuildingBlocks.Application.Contracts.Outbox;

public interface IOutboxListener
{
    Task AddAsync(OutboxMessage message);
}