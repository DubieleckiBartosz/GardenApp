namespace Panels.Application.Handlers.Commands;

public sealed class RemoveLogoHandler : ICommandHandler<RemoveLogoCommand, Response>
{
    public record RemoveLogoCommand() : ICommand<Response>;

    public Task<Response> Handle(RemoveLogoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}