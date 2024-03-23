namespace Panels.Application.Features.CreateNewPanel;

public class CreateNewPanelHandler : ICommandHandler<CreateNewPanelCommand, Response>
{
    public record CreateNewPanelCommand(string Email, string BusinessName, string BusinessUserId) : ICommand<Response>;

    public async Task<Response> Handle(CreateNewPanelCommand request, CancellationToken cancellationToken)
    {
        return Response.Ok();
    }
}