using Users.Infrastructure.Database.Domain;

namespace Users.Infrastructure.Database;

public sealed class UsersContext : DbContext
{
    internal const string UsersSchema = "users";

    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public UsersContext()
    {
    }

    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(UsersSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersContext).Assembly);
        modelBuilder.ApplyConfiguration(new OutboxMessageTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}