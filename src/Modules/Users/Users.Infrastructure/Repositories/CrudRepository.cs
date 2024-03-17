namespace Users.Infrastructure.Repositories;

internal class CrudRepository<T> : ICrudRepository<T> where T : class
{
    private readonly UsersContext _context;
    private readonly DbSet<T> _dbSet;

    public CrudRepository(UsersContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetFirstByExpressionAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}