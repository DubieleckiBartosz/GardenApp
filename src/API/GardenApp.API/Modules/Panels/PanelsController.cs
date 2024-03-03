namespace GardenApp.API.Modules.Panels;

[Route("api/[controller]")]
[ApiController]
public class PanelsController : BaseController
{
    public PanelsController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }
}