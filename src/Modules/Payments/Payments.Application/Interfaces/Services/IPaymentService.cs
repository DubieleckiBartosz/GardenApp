using Stripe;

namespace Payments.Application.Interfaces.Services;

public interface IPaymentService
{
    Task StatusProcess(Event @event);
}