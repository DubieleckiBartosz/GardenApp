namespace Works.Application.Handlers.GardeningWork;

public sealed class AddWorkItemHandler : ICommandHandler<AddWorkItemCommand, Response<AddWorkItemResponse>>
{
    public record AddWorkItemCommand(int GardeningWorkId, string Name, DateTime? EstimatedStartTime, DateTime? EstimatedEndTime) : ICommand<Response<AddWorkItemResponse>>
    {
        public static AddWorkItemCommand Create(AddWorkItemParameters parameters)
            => new(parameters.GardeningWorkId, parameters.Name, parameters.EstimatedStartTime, parameters.EstimatedEndTime);
    }

    public class AddWorkItemResponse
    {
        public int WorkItemId { get; }

        public AddWorkItemResponse(int workId)
        {
            WorkItemId = workId;
        }
    }

    private readonly IWorkItemRepository _workItemRepository;
    private readonly IGardeningWorkRepository _gardeningWorkRepository;
    private readonly ICurrentUser _currentUser;

    public AddWorkItemHandler(
        IWorkItemRepository workItemRepository,
        IGardeningWorkRepository gardeningWorkRepository,
        ICurrentUser currentUser)
    {
        _workItemRepository = workItemRepository;
        _gardeningWorkRepository = gardeningWorkRepository;
        _currentUser = currentUser;
    }

    public async Task<Response<AddWorkItemResponse>> Handle(AddWorkItemCommand request, CancellationToken cancellationToken)
    {
        var gardeningWork = await _gardeningWorkRepository.GetGardeningWorkByIdAsync(request.GardeningWorkId, cancellationToken);
        if (gardeningWork == null || gardeningWork.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(AppError.GardeningWorkNotFound(request.GardeningWorkId));
        }

        var workItem = gardeningWork.NewWorkItem(request.Name, request.EstimatedStartTime, request.EstimatedEndTime);

        await _workItemRepository.AddAsync(workItem, cancellationToken);
        await _workItemRepository.UnitOfWork.SaveAsync(cancellationToken);

        return Response<AddWorkItemResponse>.Ok(new AddWorkItemResponse(workItem.Id));
    }
}