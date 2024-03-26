namespace Panels.Application.Handlers.Commands;

public sealed class CreateProjectHandler : ICommandHandler<CreateProjectCommand, Response>
{
    public record CreateProjectCommand() : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;

    public CreateProjectHandler(IContractorRepository contractorRepository)
    {
        _contractorRepository = contractorRepository;
    }

    public Task<Response> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}