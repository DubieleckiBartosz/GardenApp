namespace Panels.Application.Handlers.Commands;

public sealed class AddLogoHandler : ICommandHandler<AddLogoCommand, Response>
{
    public record AddLogoCommand() : ICommand<Response>;

    public Task<Response> Handle(AddLogoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}