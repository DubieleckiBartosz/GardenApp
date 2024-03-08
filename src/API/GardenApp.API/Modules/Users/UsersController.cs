using static Users.Application.Integration.TestHandler;

namespace GardenApp.API.Modules.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    public UsersController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Test()
    {
        await CommandBus.Send(new TestUsersCommand());
        return NoContent();
    }
}