namespace GardenApp.API.Modules.Payments;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : BaseController
{
    public PaymentsController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }
}