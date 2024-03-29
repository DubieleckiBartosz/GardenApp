namespace Panels.Application.Handlers.Commands;

public sealed class RemoveLogoHandler : ICommandHandler<RemoveLogoCommand, Response>
{
    public record RemoveLogoCommand() : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IFileStorage _fileStorage;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public RemoveLogoHandler(
        IContractorRepository contractorRepository,
        ICurrentUser currentUser,
        IFileStorage fileStorage,
        IConfiguration configuration,
        ILogger logger)
    {
        _contractorRepository = contractorRepository;
        _currentUser = currentUser;
        _fileStorage = fileStorage;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<Response> Handle(RemoveLogoCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _contractorRepository.GetByBusinessIdAsync(_currentUser.UserId);
        if (contractor == null)
        {
            throw new NotFoundException(ErrorMessages.ContractorNotFound(_currentUser.UserId));
        }

        var logo = contractor.Logo;

        contractor.RemoveLogo();

        _logger.Error($"RemoveLogoCommand - logo key {logo!.Key}");

        await _fileStorage.RemoveFileAsync(logo!, _configuration["FileCollections:LogoImages"]!);

        await _contractorRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}