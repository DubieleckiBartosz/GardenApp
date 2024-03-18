using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Users.Application.Handlers;
using static Users.Application.Handlers.ConfirmUserHandler;
using static Users.Application.Handlers.LoginUserHandler;
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

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(LoginResponse), 200)]
    [SwaggerOperation(Summary = "User login")]
    [HttpPost("[action]")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserParameters parameters)
    {
        var response = await CommandBus.Send(LoginUserCommand.NewCommand(parameters));
        this.SetRefreshTokenInCookie(response.Data!.RefreshToken);

        return Ok(response);
    }

    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(5),
            IsEssential = true,
            SameSite = SameSiteMode.None,
            Secure = true,
        };
        Response.Cookies.Append("cookieRefreshTokenKey", refreshToken, cookieOptions);
    }
}