namespace Panels.Application.Handlers.Commands;

public sealed class RemoveProjectHandler : ICommandHandler<RemoveProjectCommand, Response>
{
    public record RemoveProjectCommand(int ProjectId) : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;
    private readonly ICurrentUser _currentUser;

    public RemoveProjectHandler(IContractorRepository contractorRepository, ICurrentUser currentUser)
    {
        _contractorRepository = contractorRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _contractorRepository.GetByBusinessIdAsync(_currentUser.UserId);
        if (contractor == null)
        {
            throw new NotFoundException(ErrorMessages.ContractorNotFound(_currentUser.UserId));
        }

        contractor.RemoveProject(request.ProjectId);
        await _contractorRepository.SaveChangesAsync();

        return Response.Ok();
    }
}