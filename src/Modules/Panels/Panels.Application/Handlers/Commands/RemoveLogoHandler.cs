namespace Panels.Application.Handlers.Commands;

public sealed class RemoveLogoHandler : ICommandHandler<RemoveLogoCommand, Response>
{
    public record RemoveLogoCommand() : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;

    public RemoveLogoHandler(IContractorRepository contractorRepository)
    {
        _contractorRepository = contractorRepository;
    }

    public Task<Response> Handle(RemoveLogoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}