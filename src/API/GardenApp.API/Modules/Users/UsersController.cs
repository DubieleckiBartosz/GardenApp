﻿namespace GardenApp.API.Modules.Users;

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
    [ProducesResponseType(typeof(Response<LoginResponse>), 200)]
    [SwaggerOperation(Summary = "User login")]
    [HttpPost("[action]")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserParameters parameters)
    {
        var response = await CommandBus.Send(LoginUserCommand.NewCommand(parameters));
        this.SetRefreshTokenInCookie(response.Data!.RefreshToken);

        return Ok(response);
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<RefreshTokenResponse>), 200)]
    [SwaggerOperation(Summary = "Refresh token")]
    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["cookieRefreshTokenKey"];
        if (refreshToken == null)
        {
            return Unauthorized();
        }

        var response = await CommandBus.Send(new RefreshTokenCommand(refreshToken));
        if (!string.IsNullOrEmpty(response.Data!.RefreshToken))
        {
            this.SetRefreshTokenInCookie(response.Data.RefreshToken);
        }

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