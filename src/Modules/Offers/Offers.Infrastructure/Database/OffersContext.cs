namespace Offers.Infrastructure.Database;

internal class OffersContext : DbContext
{
    public OffersContext(DbContextOptions options) : base(options)
    {
    }
}