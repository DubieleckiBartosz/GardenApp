namespace Panels.Application.Handlers.Commands;

public sealed class RemoveProjectImageHandler : ICommandHandler<RemoveProjectImageCommand, Response>
{
    public record RemoveProjectImageCommand() : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;

    public RemoveProjectImageHandler(IContractorRepository contractorRepository)
    {
        _contractorRepository = contractorRepository;
    }

    public Task<Response> Handle(RemoveProjectImageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}