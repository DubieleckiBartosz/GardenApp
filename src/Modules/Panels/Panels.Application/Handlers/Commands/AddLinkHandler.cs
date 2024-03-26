namespace Panels.Application.Handlers.Commands;

public sealed class AddLinkHandler : ICommandHandler<AddLinkCommand, Response>
{
    public record AddLinkCommand(string Link, LinkType LinkType) : ICommand<Response>;

    private readonly IContractorRepository _contractorRepository;
    private readonly ICurrentUser _currentUser;

    public AddLinkHandler(IContractorRepository contractorRepository, ICurrentUser currentUser)
    {
        _contractorRepository = contractorRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(AddLinkCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _contractorRepository.GetByBusinessIdAsync(_currentUser.UserId);
        if (contractor == null)
        {
            throw new NotFoundException(ErrorMessages.ContractorNotFound(_currentUser.UserId));
        }

        var link = SocialMediaLink.LinkCreator[request.LinkType].Invoke(request.Link);
        contractor.AddLink(link);

        await _contractorRepository.SaveChangesAsync();
        return Response.Ok();
    }
}