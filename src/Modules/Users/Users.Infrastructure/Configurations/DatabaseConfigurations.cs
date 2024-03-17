namespace Users.Infrastructure.Configurations;

public static class DatabaseConfigurations
{
    public static WebApplication UsersMigration(this WebApplication app)
    {
        if (app.Configuration.GetSection("UsersAutomaticMigration").Get<bool>())
        {
            app.RunMigration<UsersContext>();
        }

        return app;
    }

    public static void UsersInitData(this IHost app, IConfiguration configuration)
    {
        var dataInitOptions = configuration
            .GetSection("UsersDataInitializationOptions")
            .Get<UsersDataInitializationOptions>()!;

        if (!dataInitOptions.InsertUserData)
        {
            return;
        }

        var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory?.CreateScope();

        if (!dataInitOptions.InsertUserData && !dataInitOptions.InsertRoles && !dataInitOptions.InsertTemplates)
        {
            return;
        }

        var dataSeeder = scope?.ServiceProvider.GetService<DataSeeder>();

        if (dataInitOptions.InsertRoles)
        {
            dataSeeder?.SeedRolesAsync()?.GetAwaiter().GetResult();
        }

        if (dataInitOptions.InsertUserData)
        {
            dataSeeder?.SeedAdminAsync()?.GetAwaiter().GetResult();
        }

        if (dataInitOptions.InsertTemplates)
        {
            dataSeeder?.SeedTemplatesAsync()?.GetAwaiter().GetResult();
        }
    }
}