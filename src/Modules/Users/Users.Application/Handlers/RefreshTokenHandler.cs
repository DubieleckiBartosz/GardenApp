namespace Users.Application.Handlers;
public record RefreshTokenParameters();

public sealed class RefreshTokenHandler : ICommandHandler<RefreshTokenCommand, Response>
{
    public record RefreshTokenCommand : ICommand<Response>;

    public RefreshTokenHandler()
    {
    }

    public Task<Response> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}