namespace BuildingBlocks.Infrastructure.Database.EntityFramework;

public static class AutomaticMigration
{
    public static WebApplication RunMigration<TContext>(this WebApplication app) where TContext : DbContext
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<TContext>();

            var logger = scope.ServiceProvider
                .GetRequiredService<ILogger>();
            try
            {
                if (dbContext.Database.CanConnect())
                {
                    if (dbContext.Database.IsRelational())
                    {
                        var pendingMigrations = dbContext.Database?.GetPendingMigrations()?.ToList();
                        if (pendingMigrations != null && pendingMigrations.Any())
                        {
                            logger.Information($"Before migrations : {string.Join(", ", pendingMigrations)} - {Clock.CurrentDate()}");
                            dbContext.Database?.Migrate();
                            logger.Information($"After migrations - {Clock.CurrentDate()}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(new
                {
                    ex.Message,
                    MigrationException = ex.InnerException,
                }.Serialize());
                throw;
            }
        }

        return app;
    }
}