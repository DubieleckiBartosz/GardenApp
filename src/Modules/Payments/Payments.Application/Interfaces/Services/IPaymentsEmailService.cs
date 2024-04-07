namespace Payments.Application.Interfaces.Services;

public interface IPaymentsEmailService
{
    Task SendEmailAsync(List<string> recipients, PaymentTemplateType paymentTemplateType, Dictionary<string, string>? dictData = null);
}