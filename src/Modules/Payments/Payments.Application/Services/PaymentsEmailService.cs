namespace Payments.Application.Services;

public class PaymentsEmailService : IPaymentsEmailService
{
    private readonly IEmailClient _emailClient;
    private readonly ICrudRepository<Template> _crudRepository;
    private readonly ILogger _logger;

    public PaymentsEmailService(IEmailClient emailClient, ICrudRepository<Template> crudRepository, ILogger logger)
    {
        _emailClient = emailClient;
        _crudRepository = crudRepository;
        _logger = logger;
    }

    public async Task SendEmailAsync(List<string> recipients, PaymentTemplateType paymentTemplateType, Dictionary<string, string>? dictData = null)
    {
        var template = await _crudRepository.GetFirstByExpressionAsync(_ => _.TemplateType == (int)paymentTemplateType);
        if (template == null)
        {
            throw new NotFoundException(PaymentsErrors.TemplateNotFound);
        }

        var body = dictData == null ? template.Value : template.Value.ReplaceWithDictionary(dictData);
        await EmailProcess(recipients, template.Subject, body);
    }

    private async Task EmailProcess(List<string> recipients, string subject, string body)
    {
        var logId = Guid.NewGuid();
        _logger.Warning($"PaymentsEmailService - Sending mail with subject {subject} to {string.Join(", ", recipients)} [LogId {logId}]");

        var emailDetails = new EmailDetails(recipients, subject, body);
        await _emailClient.SendEmailAsync(emailDetails);

        _logger.Information($"PaymentsEmailService - Sent mail with subject {subject} [LogId {logId}]");
    }
}