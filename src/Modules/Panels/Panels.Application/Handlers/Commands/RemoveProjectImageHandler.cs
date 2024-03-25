namespace Panels.Application.Handlers.Commands;

public sealed class RemoveProjectImageHandler : ICommandHandler<RemoveProjectImageCommand, Response>
{
    public record RemoveProjectImageCommand() : ICommand<Response>;

    public Task<Response> Handle(RemoveProjectImageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}