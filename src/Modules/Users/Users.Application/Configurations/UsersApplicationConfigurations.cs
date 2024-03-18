namespace Users.Application.Configurations;

public static class UsersApplicationConfigurations
{
    public static WebApplicationBuilder GetUsersApplicationConfigurations(this WebApplicationBuilder builder)
    {
        builder.RegisterUsersOptions().RegisterUsersDependencyInjection();

        return builder;
    }

    private static WebApplicationBuilder RegisterUsersOptions(this WebApplicationBuilder builder)
    {
        builder.Services
            .Configure<UsersPathOptions>(builder.Configuration.GetSection("UsersPathOptions"))
            .Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

        return builder;
    }

    private static WebApplicationBuilder RegisterUsersDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUsersEmailService, UsersEmailService>();

        return builder;
    }
}