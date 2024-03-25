namespace Panels.Application.Handlers.Commands;

public sealed class AddLinkHandler : ICommandHandler<AddLinkCommand, Response>
{
    public record AddLinkCommand() : ICommand<Response>;

    public Task<Response> Handle(AddLinkCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}