namespace Panels.Application.Handlers.Commands;

public sealed class RemoveLogoHandler : ICommandHandler<RemoveLogoCommand, Response>
{
    public record RemoveLogoCommand() : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;
    private readonly ICurrentUser _currentUser;

    public RemoveLogoHandler(
        IContractorRepository contractorRepository,
        ICurrentUser currentUser)
    {
        _contractorRepository = contractorRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(RemoveLogoCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _contractorRepository.GetByBusinessIdAsync(_currentUser.UserId);
        if (contractor == null)
        {
            throw new NotFoundException(ErrorMessages.ContractorNotFound(_currentUser.UserId));
        }

        contractor.RemoveLogo();

        await _contractorRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}