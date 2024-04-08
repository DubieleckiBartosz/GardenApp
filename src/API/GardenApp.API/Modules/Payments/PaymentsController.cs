using Payments.Application.Interfaces.Services;

namespace GardenApp.API.Modules.Payments;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController
{
    private readonly IPaymentsService _paymentsService;
    private readonly IProcessService _processService;

    public PaymentsController(IPaymentsService paymentsService, IProcessService processService)
    {
        _paymentsService = paymentsService;
        _processService = processService;
    }
}