namespace Payments.Application.Configurations;

public static class PaymentsApplicationConfigurations
{
    public static WebApplicationBuilder GetPaymentsApplicationConfigurations(this WebApplicationBuilder builder)
    {
        builder.RegisterPaymentsDependencyInjection();

        return builder;
    }

    private static WebApplicationBuilder RegisterPaymentsDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPaymentsEmailService, PaymentsEmailService>();

        return builder;
    }
}