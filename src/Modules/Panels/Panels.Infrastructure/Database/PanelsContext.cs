namespace Panels.Infrastructure.Database;

internal sealed class PanelsContext : DbContext, IUnitOfWork
{
    internal const string PanelsSchema = "panels";
    private readonly IDomainDecorator _decorator;

    public DbSet<InboxMessage> InboxMessages { get; set; }
    public DbSet<Contractor> Contractors { get; set; }

    public PanelsContext()
    {
    }

    public PanelsContext(IDomainDecorator decorator, DbContextOptions<PanelsContext> options) : base(options)
    {
        _decorator = decorator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(PanelsSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PanelsContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var watcherTypes = new Type[] { typeof(Contractor) };
        var entries = ChangeTracker
            .Entries()
            .Where(e => watcherTypes.Contains(e.Entity.GetType()) && e.State
                is EntityState.Added
                or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    var currentDate = Clock.CurrentDate();
                    entityEntry.Property("Created").CurrentValue = currentDate;
                    entityEntry.Property("LastModified").CurrentValue = currentDate;
                    break;

                case EntityState.Modified:
                    entityEntry.Property("LastModified").CurrentValue = Clock.CurrentDate();
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        await this._decorator.DispatchDomainEventsAsync(this);

        return await SaveChangesAsync(cancellationToken);
    }
}