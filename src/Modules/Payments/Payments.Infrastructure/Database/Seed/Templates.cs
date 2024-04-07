namespace Payments.Infrastructure.Database.Seed;

internal static class Templates
{
    internal record TemplateResponse(string Subject, string Value);

    internal static TemplateResponse Get(PaymentTemplateType type) => type switch
    {
        PaymentTemplateType.Success => PaymentSuccessTemplate(),
        _ => throw new ArgumentOutOfRangeException(nameof(type), $"Not expected template type value: {type}"),
    };

    private static TemplateResponse PaymentSuccessTemplate()
    {
        var value = "<!DOCTYPE html><html><body>" +
                "<p>Your subscription has been successfully processed</p></body></html> ";
        var subject = "Process Payment";

        return new(subject, value);
    }
}