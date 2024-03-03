namespace GardenApp.API.Modules.Offers;

[Route("api/[controller]")]
[ApiController]
public class OffersController : BaseController
{
    public OffersController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }
}