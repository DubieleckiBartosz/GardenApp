using Users.Application.Enums;

namespace Users.Infrastructure.Database.Seed;

internal static class Templates
{
    internal record TemplateResponse(string Subject, string Value);

    internal static TemplateResponse Get(UserTemplateType type) => type switch
    {
        UserTemplateType.Confirmation => RegistrationConfirmationTemplate(),
        _ => throw new ArgumentOutOfRangeException(nameof(type), $"Not expected template type value: {type}"),
    };

    private static TemplateResponse RegistrationConfirmationTemplate()
    {
        var value = "<!DOCTYPE html><html><body><h2>Hi {UserName}</h2></br><p><strong>" +
               "Confirm your registration:<strong> " +
               "<a href={VerificationUri}>" +
               "confirmation</a></p></body></html>";
        var subject = "Confirmation";

        return new(subject, value);
    }
}