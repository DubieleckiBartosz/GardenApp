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
        var refreshToken = await _userRepository.GetRefreshTokenByValueNTAsync(request.RefreshToken)
            ?? throw new NotFoundException(StringMessages.RefreshTokenNotFound);

        var (token, refresh) = await _userRepository.GenerateAuthorizationTokensAsync(refreshToken.UserId, _settings);
        return Response<RefreshTokenResponse>.Ok(new(refresh.Value, token));
    }
}