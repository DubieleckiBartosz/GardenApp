namespace Panels.Application.Handlers.Commands;

public sealed class RemoveProjectHandler : ICommandHandler<RemoveProjectCommand, Response>
{
    public record RemoveProjectCommand(int ProjectId) : ICommand<Response>;
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUser _currentUser;

    public RemoveProjectHandler(
        ICurrentUser currentUser,
        IProjectRepository projectRepository)
    {
        _currentUser = currentUser;
        _projectRepository = projectRepository;
    }

    public async Task<Response> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByProjectIdAsync(request.ProjectId);
        if (project == null || project.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(ErrorMessages.ProjectNotFound(request.ProjectId, _currentUser.UserId));
        }

        project.Remove();

        await _projectRepository.UnitOfWork.SaveAsync(cancellationToken);

        return Response.Ok();
    }
}