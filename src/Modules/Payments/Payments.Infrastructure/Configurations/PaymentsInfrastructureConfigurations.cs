﻿namespace Payments.Infrastructure.Configurations;

public static class PaymentsInfrastructureConfigurations
{
    public static WebApplicationBuilder GetPaymentsInfrastructureConfigurations(this WebApplicationBuilder builder)
    {
        builder.RegisterDependencyInjection().PaymentsDatabaseConfiguration();

        return builder;
    }

    private static WebApplicationBuilder RegisterDependencyInjection(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));
        services.AddScoped<DataSeeder>();

        return builder;
    }
}