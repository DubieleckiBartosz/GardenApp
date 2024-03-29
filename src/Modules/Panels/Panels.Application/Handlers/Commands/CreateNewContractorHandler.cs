namespace Panels.Application.Handlers.Commands;

public sealed class CreateNewContractorHandler : ICommandHandler<CreateNewContractorCommand, Response>
{
    public record CreateNewContractorCommand(string Email, string BusinessName, string BusinessUserId, string Phone) : ICommand<Response>;

    private readonly IContractorRepository _contractorRepository;
    private readonly ICurrentUser _currentUser;

    public CreateNewContractorHandler(IContractorRepository contractorRepository, ICurrentUser currentUser)
    {
        _contractorRepository = contractorRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(CreateNewContractorCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _contractorRepository.GetByBusinessIdNTAsync(_currentUser.UserId);
        if (contractor != null)
        {
            return Response.Error(ErrorMessages.ContractorExists);
        }

        var newContractor = Contractor.CreateContractor(request.BusinessUserId, request.Email, request.BusinessName, request.Phone);
        await _contractorRepository.CreateNewContractorAsync(newContractor);
        await _contractorRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}