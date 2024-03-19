namespace Users.Application.Handlers;

public sealed class RevokeTokenHandler : ICommandHandler<RevokeTokenCommand, Response>
{
    private readonly IUserRepository _userRepository;

    public record RevokeTokenCommand(string TokenKey) : ICommand<Response>;

    public RevokeTokenHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Response> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        if (request == null || string.IsNullOrEmpty(request.TokenKey))
        {
            throw new AuthException(StringMessages.TokenIsNullOrEmpty);
        }

        var refreshToken = await this._userRepository.GetRefreshTokenByValueAsync(request.TokenKey);

        if (refreshToken == null)
        {
            throw new NotFoundException(StringMessages.RefreshTokenNotFound);
        }

        if (!refreshToken.IsActive)
        {
            throw new AuthException(StringMessages.TokenNotActive);
        }

        refreshToken.RevokeToken();
        await this._userRepository.SaveAsync();

        return Response.Ok();
    }
}