namespace Panels.Application.Handlers.Commands;

public sealed class RemoveLinkHandler : ICommandHandler<RemoveLinkCommand, Response>
{
    public record RemoveLinkCommand() : ICommand<Response>;

    public Task<Response> Handle(RemoveLinkCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}