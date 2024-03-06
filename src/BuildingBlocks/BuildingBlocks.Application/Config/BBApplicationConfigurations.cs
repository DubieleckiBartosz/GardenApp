namespace BuildingBlocks.Application.Config;

public static class BBApplicationConfigurations
{
    public static WebApplicationBuilder RegisterBBApplicationConfigurations(this WebApplicationBuilder builder)
    {
        builder
            .RegisterOptions()
            .RegisterUserAccessor();

        builder.Services.RegisterValidatorPipeline();

        return builder;
    }

    public static WebApplicationBuilder RegisterDependencyInjection(this WebApplicationBuilder builder)
    {
        //MODULE CLIENT
        builder.Services
            .AddSingleton<IModuleClient, ModuleClient>()
            .AddSingleton<IModuleSubscriber, ModuleSubscriber>()
            .AddSingleton<IModuleActionRegistration, ModuleActionRegistration>();

        //EVENT BUS
        builder.Services.AddSingleton<IEventBus>();

        return builder;
    }

    public static void LogConfigurationService(this LoggerConfiguration loggerConfiguration, IConfiguration configuration)
    {
        var logging = new LoggingOptions();
        configuration.GetSection(nameof(LoggingOptions)).Bind(logging);

        var dateTimeNowString = $"{DateTime.Now:yyyy-MM-dd}";

        loggerConfiguration
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.WithExceptionDetails()
            .Enrich.FromLogContext()
            .WriteTo.Logger(
                _ => _.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                    .WriteTo.File($"Logs/{dateTimeNowString}-Error.log",
                        rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 100000)
            )
            .WriteTo.Logger(
                _ => _.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                    .WriteTo.File($"Logs/{dateTimeNowString}-Warning.log",
                        rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 100000)
            )
            .WriteTo.File($"Logs/{dateTimeNowString}-All.log")
            .WriteTo.Console()
            .WriteTo.Seq(logging.Address!);
    }

    public static IServiceCollection RegisterMediator(this IServiceCollection services, params Type[] types)
    {
        //MEDIATOR
        Assembly[] assemblies = types.Select(type => type.Assembly).Distinct().ToArray();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        services
            .AddTransient<ICommandBus, CommandBus>()
            .AddTransient<IQueryBus, QueryBus>();

        return services;
    }

    public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
    => app.ApplicationServices.GetRequiredService<IModuleSubscriber>();

    #region Private Methods

    private static WebApplicationBuilder RegisterOptions(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));

        return builder;
    }

    private static WebApplicationBuilder RegisterUserAccessor(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddHttpContextAccessor()
            .AddTransient<ICurrentUser, CurrentUser>();

        return builder;
    }

    private static IServiceCollection RegisterValidatorPipeline(this IServiceCollection services)
    {
        //VALIDATOR PIPELINE
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        return services;
    }

    #endregion Private Methods
}