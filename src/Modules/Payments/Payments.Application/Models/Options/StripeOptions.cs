namespace Payments.Application.Models.Options;

internal class StripeOptions
{
    public string ApiKey { get; set; }
    public string WebhookSecret { get; set; }
}