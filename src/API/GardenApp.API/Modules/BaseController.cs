namespace GardenApp.API.Modules;

public class BaseController : ControllerBase
{
    protected readonly ICommandBus CommandBus;
    protected readonly IQueryBus QueryBus;

    public BaseController(ICommandBus commandBus, IQueryBus queryBus)
    {
        CommandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        QueryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
    }
}