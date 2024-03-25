namespace Panels.Application.Handlers.Commands;

public sealed class RemoveProjectHandler : ICommandHandler<RemoveProjectCommand, Response>
{
    public record RemoveProjectCommand() : ICommand<Response>;

    public Task<Response> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}