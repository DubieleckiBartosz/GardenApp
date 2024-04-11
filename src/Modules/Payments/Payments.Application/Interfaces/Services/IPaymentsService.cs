using BuildingBlocks.Application.Wrappers;
using Payments.Application.Models.Responses;

namespace Payments.Application.Interfaces.Services;

public interface IPaymentsService
{
    Task CreateNewPayer();

    Task CancelSubscription();

    Task ContinueCanceledSubscription();

    Task<Response<CreateCheckoutSessionResponse>> CreateCheckoutAsync(CreateCheckoutSessionParameters parameters, string baseUrl);

    Task SessionCompleted(Session session);
}