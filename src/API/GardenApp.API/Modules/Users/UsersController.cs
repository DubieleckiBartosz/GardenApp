using Microsoft.AspNetCore.Authorization;
using static Users.Application.Handlers.ConfirmUserHandler;
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

    [AllowAnonymous]
    [HttpGet("confirm-user")]
    public async Task<IActionResult> ConfirmUser(string code, string email)
    {
        var result = await CommandBus.Send(new ConfirmUserCommand(code, email));
        return result.Success ? Ok(result) : BadRequest(result);
    }
}