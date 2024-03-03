namespace GardenApp.API.Modules.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    public UsersController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }
}