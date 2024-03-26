namespace Panels.Application.Handlers.Commands;

public sealed class AddLinkHandler : ICommandHandler<AddLinkCommand, Response>
{
    public record AddLinkCommand() : ICommand<Response>;

    private readonly IContractorRepository _contractorRepository;

    public AddLinkHandler(IContractorRepository contractorRepository)
    {
        _contractorRepository = contractorRepository;
    }

    public Task<Response> Handle(AddLinkCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}