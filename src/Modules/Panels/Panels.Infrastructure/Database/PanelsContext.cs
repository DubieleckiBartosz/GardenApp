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
}