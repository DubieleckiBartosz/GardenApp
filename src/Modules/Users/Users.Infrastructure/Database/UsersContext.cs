using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Users.Infrastructure.Database;

public sealed class UsersContext : IdentityDbContext<User>
{
    internal const string UsersSchema = "users";

    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<User> ApplicationUsers { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

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