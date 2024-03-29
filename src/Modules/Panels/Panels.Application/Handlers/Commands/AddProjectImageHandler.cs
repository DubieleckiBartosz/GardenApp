namespace Panels.Application.Handlers.Commands;

public sealed class AddProjectImageHandler : ICommandHandler<AddProjectImageCommand, Response>
{
    public record AddProjectImageCommand(IFormFile FormFile, int ProjectId) : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IFileStorage _fileStorage;
    private readonly IConfiguration _configuration;

    public AddProjectImageHandler(
        IContractorRepository contractorRepository,
        ICurrentUser currentUser,
        IFileStorage fileStorage,
        IConfiguration configuration)
    {
        _contractorRepository = contractorRepository;
        _currentUser = currentUser;
        _fileStorage = fileStorage;
        _configuration = configuration;
    }

    public async Task<Response> Handle(AddProjectImageCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _contractorRepository.GetByBusinessIdAsync(_currentUser.UserId);
        if (contractor == null)
        {
            throw new NotFoundException(ErrorMessages.ContractorNotFound(_currentUser.UserId));
        }

        var uniqueName = request.FormFile.CreateName();
        contractor.AddProjectImage(request.ProjectId, uniqueName);
        var error = await _fileStorage.Save(request.FormFile, uniqueName, _configuration["FileCollections:ProjectImages"]!);
        if (error != null)
        {
            throw new BadRequestException(error);
        }

        await _contractorRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}