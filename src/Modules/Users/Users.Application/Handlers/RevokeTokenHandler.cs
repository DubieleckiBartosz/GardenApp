namespace Users.Application.Handlers;
public record RevokeTokenParameters();

public sealed class RevokeTokenHandler : ICommandHandler<RevokeTokenCommand, Unit>
{
    public record RevokeTokenCommand : ICommand<Unit>;

    public RevokeTokenHandler()
    {
    }

    public Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}