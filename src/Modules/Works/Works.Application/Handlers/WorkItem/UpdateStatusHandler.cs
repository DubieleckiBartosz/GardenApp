using Works.Application.Interfaces.Repositories;

namespace Works.Application.Handlers.WorkItem;

public sealed class UpdateStatusHandler : ICommandHandler<WorkItemUpdateStatusCommand, Response>
{
    public record WorkItemUpdateStatusCommand(int WorkItemId, int Status) : ICommand<Response>
    {
        public static WorkItemUpdateStatusCommand Create(WorkItemUpdateStatusParameters parameters)
            => new(parameters.WorkItemId, parameters.Status);
    }

    private readonly IWorkItemRepository _workItemRepository;
    private readonly ICurrentUser _currentUser;

    public UpdateStatusHandler(IWorkItemRepository workItemRepository, ICurrentUser currentUser)
    {
        _workItemRepository = workItemRepository;
        _currentUser = currentUser;
    }

    public Task<Response> Handle(WorkItemUpdateStatusCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}