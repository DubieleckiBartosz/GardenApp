using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Infrastructure.Config;

public static class BBInfrastructureConfigurations
{
    public static IServiceCollection RegisterEntityFrameworkNpg<T>(this IServiceCollection services, string connectionString, string schema,
        Func<DbContextOptionsBuilder, DbContextOptionsBuilder>? additionalRegistrations = null, ILoggerFactory? loggerFactory = null) where T : DbContext
    {
        services.AddDbContext<T>(dbContextBuilder =>
        {
            dbContextBuilder.UseNpgsql(connectionString, _ => { _.MigrationsHistoryTable("__EFMigrationsHistory", schema); });
            if (loggerFactory != null)
            {
                dbContextBuilder.UseLoggerFactory(loggerFactory);
            }

            additionalRegistrations?.Invoke(dbContextBuilder);
        });

        return services;
    }
}