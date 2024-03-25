namespace Panels.Application.Handlers.Commands;

public sealed class CreateProjectHandler : ICommandHandler<CreateProjectCommand, Response>
{
    public record CreateProjectCommand() : ICommand<Response>;

    public Task<Response> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}