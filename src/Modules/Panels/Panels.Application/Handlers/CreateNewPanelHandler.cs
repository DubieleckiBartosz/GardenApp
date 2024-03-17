namespace Panels.Application.Features.CreateNewPanel;

public class CreateNewPanelHandler : ICommandHandler<CreateNewPanelCommand, Response>
{
    public record CreateNewPanelCommand() : ICommand<Response>;

    public Task<Response> Handle(CreateNewPanelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}