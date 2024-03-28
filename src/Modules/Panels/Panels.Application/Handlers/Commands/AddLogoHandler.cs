namespace Panels.Application.Handlers.Commands;

public sealed class AddLogoHandler : ICommandHandler<AddLogoCommand, Response>
{
    public record AddLogoCommand(IFormFile FormFile) : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IFileStorage _fileStorage;
    private readonly IConfiguration _configuration;

    public AddLogoHandler(
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

    public async Task<Response> Handle(AddLogoCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _contractorRepository.GetByBusinessIdAsync(_currentUser.UserId);
        if (contractor == null)
        {
            throw new NotFoundException(ErrorMessages.ContractorNotFound(_currentUser.UserId));
        }

        var uniqueName = request.FormFile.CreateName();
        var error = await _fileStorage.Save(request.FormFile, uniqueName, _configuration["FileCollections:ProjectImages"]!);
        if (error != null)
        {
            throw new BadRequestException(error);
        }

        contractor.AddLogo(uniqueName);
        await _contractorRepository.SaveChangesAsync();

        return Response.Ok();
    }
}