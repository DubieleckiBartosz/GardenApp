namespace Panels.Application.Handlers.Commands;

public sealed class RemoveProjectHandler : ICommandHandler<RemoveProjectCommand, Response>
{
    public record RemoveProjectCommand() : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;

    public RemoveProjectHandler(IContractorRepository contractorRepository)
    {
        _contractorRepository = contractorRepository;
    }

    public Task<Response> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}