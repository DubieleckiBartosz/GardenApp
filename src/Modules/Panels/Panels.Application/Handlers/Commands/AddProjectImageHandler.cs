namespace Panels.Application.Handlers.Commands;

public sealed class AddProjectImageHandler : ICommandHandler<AddProjectImageCommand, Response>
{
    public record AddProjectImageCommand(IFormFile FormFile, int ProjectId) : ICommand<Response>;
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IFileStorage _fileStorage;
    private readonly IConfiguration _configuration;

    public AddProjectImageHandler(
        IProjectRepository projectRepository,
        ICurrentUser currentUser,
        IFileStorage fileStorage,
        IConfiguration configuration)
    {
        _projectRepository = projectRepository;
        _currentUser = currentUser;
        _fileStorage = fileStorage;
        _configuration = configuration;
    }

    public async Task<Response> Handle(AddProjectImageCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByProjectIdAsync(request.ProjectId);
        if (project == null || project.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(ErrorMessages.ProjectNotFound(request.ProjectId, _currentUser.UserId));
        }

        var uniqueName = request.FormFile.CreateName();
        project.AddImage(uniqueName);
        var error = await _fileStorage.Save(request.FormFile, uniqueName, _configuration["FileCollections:ProjectImages"]!);
        if (error != null)
        {
            throw new BadRequestException(error);
        }

        await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}