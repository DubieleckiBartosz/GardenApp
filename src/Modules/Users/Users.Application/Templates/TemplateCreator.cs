namespace Users.Application.Templates;

internal static class TemplateCreator
{
    internal static Dictionary<string, string> TemplateRegisterAccount(string userName, string code) => new Dictionary<string, string>
        {
            {"UserName", userName},
            {"VerificationUri", code}
        };
}