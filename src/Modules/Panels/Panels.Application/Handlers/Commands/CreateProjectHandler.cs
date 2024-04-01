namespace Panels.Application.Handlers.Commands;

public sealed class CreateProjectHandler : ICommandHandler<CreateProjectCommand, Response<CreateProjectResponse>>
{
    public class CreateProjectResponse
    {
        public int ProjectId { get; }

        public CreateProjectResponse(int projectId)
        {
            ProjectId = projectId;
        }
    }

    public record CreateProjectCommand(string Description) : ICommand<Response<CreateProjectResponse>>;
    private readonly IContractorRepository _contractorRepository;
    private readonly IProjectRepository _projectRepository;

    private readonly ICurrentUser _currentUser;

    public CreateProjectHandler(
        IContractorRepository contractorRepository,
        ICurrentUser currentUser,
        IProjectRepository projectRepository)
    {
        _contractorRepository = contractorRepository;
        _currentUser = currentUser;
        _projectRepository = projectRepository;
    }

    public async Task<Response<CreateProjectResponse>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _contractorRepository.GetByBusinessIdNTAsync(_currentUser.UserId);
        if (contractor == null)
        {
            throw new NotFoundException(ErrorMessages.ContractorNotFound(_currentUser.UserId));
        }

        var project = contractor.AddNewProject(request.Description);

        await _projectRepository.CreateNewProjectAsync(project);
        await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response<CreateProjectResponse>.Ok(new CreateProjectResponse(project.Id));
    }
}