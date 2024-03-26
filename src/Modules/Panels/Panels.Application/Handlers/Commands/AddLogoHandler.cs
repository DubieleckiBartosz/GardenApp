namespace Panels.Application.Handlers.Commands;

public sealed class AddLogoHandler : ICommandHandler<AddLogoCommand, Response>
{
    public record AddLogoCommand() : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;

    public AddLogoHandler(IContractorRepository contractorRepository)
    {
        _contractorRepository = contractorRepository;
    }

    public Task<Response> Handle(AddLogoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}