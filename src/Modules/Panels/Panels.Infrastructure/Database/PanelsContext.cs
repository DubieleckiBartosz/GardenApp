namespace Panels.Infrastructure.Database;

internal sealed class PanelsContext : DbContext
{
    internal const string PanelsSchema = "panels";

    public DbSet<InboxMessage> InboxMessages { get; set; }

    public PanelsContext()
    {
    }

    public PanelsContext(DbContextOptions<PanelsContext> options) : base(options)
    {
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
                or EntityState.Modified
                or EntityState.Deleted);

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
}