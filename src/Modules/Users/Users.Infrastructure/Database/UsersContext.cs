namespace Users.Infrastructure.Database;

internal class UsersContext : DbContext
{
    internal const string UsersSchema = "users";

    internal DbSet<OutboxMessage> OutboxMessages { get; set; }

    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }
}