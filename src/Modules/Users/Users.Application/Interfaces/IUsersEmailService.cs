namespace Users.Application.Interfaces;

public interface IUsersEmailService
{
    Task SendEmailAsync(List<string> recipients, Dictionary<string, string> dictData, UserTemplateType userTemplateType);
}