namespace Users.Infrastructure.Database.Seed;

internal static class Templates
{
    internal record TemplateResponse(string Subject, string Value);

    internal static TemplateResponse Get(UserTemplateType type) => type switch
    {
        UserTemplateType.Confirmation => RegistrationConfirmationTemplate(),
        UserTemplateType.ResetPassword => ResetPassowrdTemplate(),
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

    private static TemplateResponse ResetPassowrdTemplate()
    {
        var value = "<!DOCTYPE html><html><body><h4>Reset Password Email</h4>" +
                "<p>Please use the below token to reset your password with the <code>" +
                "{Path}</code>api route: </p>" +
                "<p><code>{ResetToken}</code></p></body></html> ";
        var subject = "Reset Password";

        return new(subject, value);
    }
}