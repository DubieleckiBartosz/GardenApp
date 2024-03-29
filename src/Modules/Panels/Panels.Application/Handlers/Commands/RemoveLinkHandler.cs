namespace Panels.Application.Handlers.Commands;

public sealed class RemoveLinkHandler : ICommandHandler<RemoveLinkCommand, Response>
{
    public record RemoveLinkCommand(LinkType LinkType) : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;
    private readonly ICurrentUser _currentUser;

    public RemoveLinkHandler(IContractorRepository contractorRepository, ICurrentUser currentUser)
    {
        _contractorRepository = contractorRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(RemoveLinkCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _contractorRepository.GetByBusinessIdAsync(_currentUser.UserId);
        if (contractor == null)
        {
            throw new NotFoundException(ErrorMessages.ContractorNotFound(_currentUser.UserId));
        }

        contractor.RemoveLink(request.LinkType);
        await _contractorRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}