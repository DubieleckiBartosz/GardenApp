using BuildingBlocks.Infrastructure.Inbox;
using Microsoft.EntityFrameworkCore;

namespace Panels.Infrastructure.Database;

internal sealed class PanelsContext : DbContext
{
    internal const string PanelsSchema = "panels";

    internal DbSet<InboxMessage> InboxMessages { get; set; }

    public PanelsContext(DbContextOptions<PanelsContext> options) : base(options)
    {
    }
}