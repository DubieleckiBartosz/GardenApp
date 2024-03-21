namespace Offers.Infrastructure.Database;

internal class OffersContext : DbContext, IUnitOfWork
{
    internal const string OffersSchema = "offers";
    internal DbSet<GardenOffer> GardenOffers { get; set; }

    internal DbSet<GardenOfferItem> GardenOfferItems { get; set; }

    public OffersContext(DbContextOptions<OffersContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(OffersSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OffersContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var watcherTypes = new Type[] { typeof(GardenOfferItem), typeof(GardenOffer) };
        var entries = ChangeTracker
            .Entries()
            .Where(e => watcherTypes.Contains(e.Entity.GetType()) && e.State is EntityState.Added or EntityState.Modified);

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

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default) =>
        await SaveChangesAsync(cancellationToken);
}