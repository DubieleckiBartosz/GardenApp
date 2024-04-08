namespace Payments.Application.Configurations;

internal static class StripeConfigurations
{
    internal static WebApplicationBuilder RegisterStripeConfigurations(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        var services = builder.Services;

        StripeConfiguration.ApiKey = config["StripeOptions:ApiKey"];
        services.Configure<StripeOptions>(config);

        var appInfo = new AppInfo
        {
            Name = "StripeGardenApp",
            Version = "0.1.0"
        };

        StripeConfiguration.AppInfo = appInfo;

        services.AddHttpClient("Stripe");

        services.AddScoped<IStripeClient, StripeClient>(s =>
        {
            var clientFactory = s.GetRequiredService<IHttpClientFactory>();
            var httpClient = new SystemNetHttpClient(
               httpClient: clientFactory.CreateClient("Stripe"),
               maxNetworkRetries: StripeConfiguration.MaxNetworkRetries,
               appInfo: appInfo,
               enableTelemetry: StripeConfiguration.EnableTelemetry);

            return new StripeClient(apiKey: StripeConfiguration.ApiKey, httpClient: httpClient);
        });

        return builder;
    }
}