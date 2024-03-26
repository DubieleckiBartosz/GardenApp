namespace Panels.Application.Handlers.Commands;

public sealed class UpdateProjectDescriptionHandler : ICommandHandler<UpdateProjectDescriptionCommand, Response>
{
    public record UpdateProjectDescriptionCommand(int ProjectId, string Description) : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;
    private readonly ICurrentUser _currentUser;

    public UpdateProjectDescriptionHandler(IContractorRepository contractorRepository, ICurrentUser currentUser)
    {
        _contractorRepository = contractorRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(UpdateProjectDescriptionCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _contractorRepository.GetByBusinessIdAsync(_currentUser.UserId);
        if (contractor == null)
        {
            throw new NotFoundException(ErrorMessages.ContractorNotFound(_currentUser.UserId));
        }

        contractor.UpdateProjectDescription(request.ProjectId, request.Description);
        await _contractorRepository.SaveChangesAsync();

        return Response.Ok();
    }
}