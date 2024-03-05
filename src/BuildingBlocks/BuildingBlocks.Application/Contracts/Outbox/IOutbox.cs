using BuildingBlocks.Application.Models.Outbox;

namespace BuildingBlocks.Application.Contracts.Outbox;

public interface IOutbox
{
    Task AddAsync(OutboxMessage message);
}