namespace Payments.Application.Models.Options;

public class PaymentsDataInitializationOptions
{
    public bool InsertTemplates { get; set; }
    public bool CreateStripeProduct { get; set; }
}