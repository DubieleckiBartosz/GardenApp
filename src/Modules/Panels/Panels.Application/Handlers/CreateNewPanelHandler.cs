using Panels.Domain.Contractors;

namespace Panels.Application.Features.CreateNewPanel;

public class CreateNewPanelHandler : ICommandHandler<CreateNewPanelCommand, Response>
{
    public record CreateNewPanelCommand(string Email, string BusinessName, string BusinessUserId, string Phone) : ICommand<Response>;

    public async Task<Response> Handle(CreateNewPanelCommand request, CancellationToken cancellationToken)
    {
        var newContractor = Contractor.CreateContractor(request.BusinessUserId, request.Email, request.BusinessName, request.Phone);
        return Response.Ok();
    }
}