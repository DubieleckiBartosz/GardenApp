namespace Users.Application.Interfaces;

internal interface IUsersEmailService
{
    Task SendEmailAsync(List<string> recipients, Dictionary<string, string> dictData, UserTemplateType userTemplateType);
}