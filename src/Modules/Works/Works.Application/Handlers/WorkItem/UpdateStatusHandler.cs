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

    public async Task<Response> Handle(WorkItemUpdateStatusCommand request, CancellationToken cancellationToken)
    {
        var workItem = await _workItemRepository.GetWorkItemWithRecordsByIdAsync(request.WorkItemId, cancellationToken);
        if (workItem == null || workItem.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(AppError.WorkItemNotFound(request.WorkItemId));
        }

        var status = Enumeration.GetById<WorkItemStatus>(request.Status);
        workItem.UpdateStatus(status);

        await _workItemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}