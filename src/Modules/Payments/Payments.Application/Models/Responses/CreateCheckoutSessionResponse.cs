namespace Payments.Application.Models.Responses;

public class CreateCheckoutSessionResponse
{
    public string PaymentUrl { get; }

    public CreateCheckoutSessionResponse(string paymentUrl)
    {
        PaymentUrl = paymentUrl;
    }
}