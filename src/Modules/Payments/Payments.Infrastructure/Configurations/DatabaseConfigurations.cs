namespace Payments.Infrastructure.Configurations;

public static class DatabaseConfigurations
{
    internal static WebApplicationBuilder PaymentsDatabaseConfiguration(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var options = builder.Configuration.GetSection("EfOptions").Get<EfOptions>()!;
        var schema = PaymentsContext.PaymentsSchema;
        var connectionString = options.ConnectionString + schema;

        builder.RegisterEntityFrameworkNpg<PaymentsContext>(connectionString, schema);

        return builder;
    }

    public static WebApplication PaymentsMigration(this WebApplication app)
    {
        if (app.Configuration.GetSection("PaymentsAutomaticMigration").Get<bool>())
        {
            app.RunMigration<PaymentsContext>();
        }

        return app;
    }

    public static void PaymentsInitData(this IHost app, IConfiguration configuration)
    {
        var dataInitOptions = configuration
            .GetSection("PaymentsDataInitializationOptions")
            .Get<PaymentsDataInitializationOptions>()!;

        var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory?.CreateScope();

        var dataSeeder = scope?.ServiceProvider.GetService<DataSeeder>();

        if (dataInitOptions.InsertTemplates)
        {
            dataSeeder?.SeedTemplatesAsync()?.GetAwaiter().GetResult();
        }
    }
}