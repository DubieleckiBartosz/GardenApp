namespace Panels.Application.Handlers.Commands;

public sealed class UpdateProjectDescriptionHandler : ICommandHandler<UpdateProjectDescriptionCommand, Response>
{
    public record UpdateProjectDescriptionCommand() : ICommand<Response>;

    public Task<Response> Handle(UpdateProjectDescriptionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}