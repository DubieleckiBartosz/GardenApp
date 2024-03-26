namespace Panels.Application.Handlers.Commands;

public sealed class UpdateProjectDescriptionHandler : ICommandHandler<UpdateProjectDescriptionCommand, Response>
{
    public record UpdateProjectDescriptionCommand() : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;

    public UpdateProjectDescriptionHandler(IContractorRepository contractorRepository)
    {
        _contractorRepository = contractorRepository;
    }

    public Task<Response> Handle(UpdateProjectDescriptionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}