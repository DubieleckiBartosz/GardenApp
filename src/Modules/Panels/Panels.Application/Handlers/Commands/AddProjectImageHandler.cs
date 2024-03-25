namespace Panels.Application.Handlers.Commands;

public sealed class AddProjectImageHandler : ICommandHandler<AddProjectImageCommand, Response>
{
    public record AddProjectImageCommand() : ICommand<Response>;

    public Task<Response> Handle(AddProjectImageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}