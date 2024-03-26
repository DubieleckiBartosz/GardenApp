namespace Panels.Application.Handlers.Commands;

public sealed class RemoveLinkHandler : ICommandHandler<RemoveLinkCommand, Response>
{
    public record RemoveLinkCommand() : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;

    public RemoveLinkHandler(IContractorRepository contractorRepository)
    {
        _contractorRepository = contractorRepository;
    }

    public Task<Response> Handle(RemoveLinkCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}