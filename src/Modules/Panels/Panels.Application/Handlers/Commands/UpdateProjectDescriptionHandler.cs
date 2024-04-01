namespace Panels.Application.Handlers.Commands;

public sealed class UpdateProjectDescriptionHandler : ICommandHandler<UpdateProjectDescriptionCommand, Response>
{
    public record UpdateProjectDescriptionCommand(int ProjectId, string Description) : ICommand<Response>;
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUser _currentUser;

    public UpdateProjectDescriptionHandler(
        ICurrentUser currentUser,
        IProjectRepository projectRepository)
    {
        _currentUser = currentUser;
        _projectRepository = projectRepository;
    }

    public async Task<Response> Handle(UpdateProjectDescriptionCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByProjectIdAsync(request.ProjectId);
        if (project == null || project.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(ErrorMessages.ProjectNotFound(request.ProjectId, _currentUser.UserId));
        }

        project.UpdateDescription(request.Description);
        await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}