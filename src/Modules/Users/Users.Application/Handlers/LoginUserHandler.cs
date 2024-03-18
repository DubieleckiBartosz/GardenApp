namespace Users.Application.Handlers;
public record LoginUserParameters(string Email, string Password);

public sealed class LoginUserHandler : ICommandHandler<LoginUserCommand, Response<LoginResponse>>
{
    public record LoginResponse(string RefreshToken, string AuthorizationToken);
    public record LoginUserCommand(string Email, string Password) : ICommand<Response<LoginResponse>>
    {
        public static LoginUserCommand NewCommand(LoginUserParameters parameters)
        {
            return new(parameters.Email, parameters.Password);
        }
    }

    private readonly IUserRepository _userRepository;
    private readonly JwtSettings _settings;

    public LoginUserHandler(IUserRepository userRepository, IOptions<JwtSettings> options)
    {
        _userRepository = userRepository;
        _settings = options!.Value;
    }

    public async Task<Response<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user == null)
        {
            throw new AuthException(StringMessages.Unauthorized, HttpStatusCode.Unauthorized);
        }

        var blocked = await _userRepository.UserIsBlockedAsync(user, request.Password);
        if (blocked)
        {
            throw new AuthException(StringMessages.UserBlocked, HttpStatusCode.Forbidden);
        }

        var passwordValid = await _userRepository.CheckPasswordAsync(user, request.Password);

        if (!passwordValid)
        {
            throw new AuthException(StringMessages.PasswordInvalid, HttpStatusCode.BadRequest);
        }

        var (token, refresh) = await _userRepository.GenerateAuthorizationTokensAsync(user.Id, _settings);

        return Response<LoginResponse>.Ok(new LoginResponse(refresh.Value, token));
    }
}