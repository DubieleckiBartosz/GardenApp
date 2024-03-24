namespace Panels.Application.Handlers;

public class CreateNewPanelHandler : ICommandHandler<CreateNewPanelCommand, Response>
{
    public record CreateNewPanelCommand(string Email, string BusinessName, string BusinessUserId, string Phone) : ICommand<Response>;

    private readonly IContractorRepository _contractorRepository;
    private readonly ICurrentUser _currentUser;

    public CreateNewPanelHandler(IContractorRepository contractorRepository, ICurrentUser currentUser)
    {
        _contractorRepository = contractorRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(CreateNewPanelCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _contractorRepository.GetByBusinessIdNTAsync(_currentUser.UserId);
        if (contractor != null)
        {
            return Response.Error(ErrorMessages.ContractorExists);
        }

        var newContractor = Contractor.CreateContractor(request.BusinessUserId, request.Email, request.BusinessName, request.Phone);
        await _contractorRepository.CreateNewContractorAsync(newContractor);

        return Response.Ok();
    }
}