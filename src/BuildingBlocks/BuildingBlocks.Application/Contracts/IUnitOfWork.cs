namespace BuildingBlocks.Application.Contracts;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
}