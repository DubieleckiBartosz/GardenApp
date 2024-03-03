namespace GardenApp.API.Modules.Analytics;

[Route("api/[controller]")]
[ApiController]
public class AnalyticsController : BaseController
{
    public AnalyticsController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }
}