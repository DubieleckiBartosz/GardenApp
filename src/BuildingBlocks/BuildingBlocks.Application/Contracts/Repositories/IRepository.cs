namespace BuildingBlocks.Application.Contracts.Repositories;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}