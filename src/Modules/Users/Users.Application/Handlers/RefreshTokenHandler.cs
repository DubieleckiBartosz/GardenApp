namespace Users.Application.Handlers;

public sealed class RefreshTokenHandler : ICommandHandler<RefreshTokenCommand, Response<RefreshTokenResponse>>
{
    public record RefreshTokenCommand(string RefreshToken) : ICommand<Response<RefreshTokenResponse>>;
    public record RefreshTokenResponse(string RefreshToken, string AuthorizationToken);

    private readonly IUserRepository _userRepository;

    private readonly JwtSettings _settings;

    public RefreshTokenHandler(IUserRepository userRepository, IOptions<JwtSettings> options)
    {
        _userRepository = userRepository;
        _settings = options!.Value;
    }

    public async Task<Response<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenResult = await _userRepository.GetRefreshTokenByValueNTAsync(request.RefreshToken)
            ?? throw new NotFoundException(StringMessages.RefreshTokenNotFound);

        if (!refreshTokenResult.IsActive)
        {
            throw new AuthException(StringMessages.TokenNotActive);
        }

        var userWithTokens = await _userRepository.GetUserWithRefreshTokensByIdAsync(refreshTokenResult.UserId)
            ?? throw new NotFoundException(StringMessages.UserNotFound);

        var token = await _userRepository.GenerateAuthorizationTokenAsync(userWithTokens, _settings);

        var newRefreshToken = userWithTokens.NewRefreshToken(request.RefreshToken);

        await _userRepository.SaveAsync();

        return Response<RefreshTokenResponse>.Ok(new(newRefreshToken.Value, token));
    }
}