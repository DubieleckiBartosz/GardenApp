namespace GardenApp.API.Modules.Works;

[Route("api/[controller]")]
[ApiController]
public class WorksController : BaseController
{
    public WorksController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }
}