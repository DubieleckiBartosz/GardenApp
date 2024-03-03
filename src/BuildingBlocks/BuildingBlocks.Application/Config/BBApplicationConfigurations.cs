using BuildingBlocks.Application.Clients.Smtp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace BuildingBlocks.Application.Config;

public static class BBApplicationConfigurations
{
    public static WebApplicationBuilder RegisterOptions(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));

        return builder;
    }

    public static WebApplicationBuilder RegisterDependencyInjection(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        var options = configuration.GetSection("EmailOptions").Get<EmailOptions>();
        if (options!.Enabled)
        {
            services.AddScoped<IEmailClient, LocalEmailClient>();
        }
        else
        {
            throw new NotImplementedException("Real EmailClient is not implemented.");
        }

        return builder;
    }
}