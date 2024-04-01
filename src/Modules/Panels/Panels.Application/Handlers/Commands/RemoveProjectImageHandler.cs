namespace Panels.Application.Handlers.Commands;

public sealed class RemoveProjectImageHandler : ICommandHandler<RemoveProjectImageCommand, Response>
{
    public record RemoveProjectImageCommand(int ProjectId, string ImageKey) : ICommand<Response>;
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUser _currentUser;

    public RemoveProjectImageHandler(
        ICurrentUser currentUser,
        IProjectRepository projectRepository)
    {
        _currentUser = currentUser;
        _projectRepository = projectRepository;
    }

    public async Task<Response> Handle(RemoveProjectImageCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByProjectIdAsync(request.ProjectId);
        if (project == null || project.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(ErrorMessages.ProjectNotFound(request.ProjectId, _currentUser.UserId));
        }

        project.RemoveImage(request.ImageKey);

        await _projectRepository.UnitOfWork.SaveAsync(cancellationToken);

        return Response.Ok();
    }
}