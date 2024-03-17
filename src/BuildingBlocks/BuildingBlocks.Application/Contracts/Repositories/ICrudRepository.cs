using System.Linq.Expressions;

namespace BuildingBlocks.Application.Contracts.Repositories;

public interface ICrudRepository<T> where T : class
{
    Task<T?> GetFirstByExpressionAsync(Expression<Func<T, bool>> predicate);

    Task AddAsync(T aggregate);
}