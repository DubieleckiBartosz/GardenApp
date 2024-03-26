namespace Panels.Application.Handlers.Commands;

public sealed class CreateProjectHandler : ICommandHandler<CreateProjectCommand, Response>
{
    public record CreateProjectCommand(string Description) : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;
    private readonly ICurrentUser _currentUser;

    public CreateProjectHandler(IContractorRepository contractorRepository, ICurrentUser currentUser)
    {
        _contractorRepository = contractorRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _contractorRepository.GetByBusinessIdAsync(_currentUser.UserId);
        if (contractor == null)
        {
            throw new NotFoundException(ErrorMessages.ContractorNotFound(_currentUser.UserId));
        }

        contractor.AddNewProject(request.Description);
        await _contractorRepository.SaveChangesAsync();

        return Response.Ok();
    }
}