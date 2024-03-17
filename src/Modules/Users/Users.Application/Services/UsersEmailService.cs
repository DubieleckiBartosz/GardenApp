namespace Users.Application.Services;

internal class UsersEmailService : IUsersEmailService
{
    private readonly IEmailClient _emailClient;
    private readonly ICrudRepository<Template> _crudRepository;

    public UsersEmailService(IEmailClient emailClient, ICrudRepository<Template> crudRepository)
    {
        _emailClient = emailClient;
        _crudRepository = crudRepository;
    }

    public async Task SendEmailAsync(List<string> recipients, Dictionary<string, string> dictData, UserTemplateType userTemplateType)
    {
        var template = await _crudRepository.GetFirstByExpressionAsync(_ => _.TemplateType == (int)userTemplateType);
        if (template == null)
        {
            throw new NotFoundException(StringMessages.TemplateNotFound);
        }

        var body = template.Value.ReplaceWithDictionary(dictData);
        await EmailProcess(recipients, template.Subject, body);
    }

    private async Task EmailProcess(List<string> recipients, string subject, string body)
    {
        var emailDetails = new EmailDetails(recipients, subject, body);
        await _emailClient.SendEmailAsync(emailDetails);
    }
}