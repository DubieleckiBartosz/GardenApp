namespace Works.Infrastructure.Database;

internal class WorksContext : DbContext, IUnitOfWork
{
    internal const string WorksSchema = "works";
    private readonly IDomainDecorator _decorator;

    public WorksContext()
    {
    }

    public WorksContext(IDomainDecorator decorator, DbContextOptions<WorksContext> options) : base(options)
    {
        _decorator = decorator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(WorksSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorksContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //TODO
        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        await this._decorator.DispatchDomainEventsAsync(this);

        return await SaveChangesAsync(cancellationToken);
    }
}