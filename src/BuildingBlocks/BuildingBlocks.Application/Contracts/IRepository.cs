using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Application.Contracts;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}