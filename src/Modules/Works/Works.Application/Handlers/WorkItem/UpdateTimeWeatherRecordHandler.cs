namespace Works.Application.Handlers.WorkItem;

public sealed class UpdateTimeWeatherRecordHandler : ICommandHandler<UpdateTimeWeatherRecordCommand, Response>
{
    public record UpdateTimeWeatherRecordCommand(int WorkItemId, int TimeWeatherRecordId, int Minutes, DateTime Date) : ICommand<Response>
    {
        public static UpdateTimeWeatherRecordCommand Create(UpdateTimeWeatherRecordParameters parameters)
            => new(
                parameters.WorkItemId,
                parameters.TimeWeatherRecordId,
                parameters.Minutes,
                parameters.Date);
    }

    private readonly IWorkItemRepository _workItemRepository;
    private readonly ICurrentUser _currentUser;

    public UpdateTimeWeatherRecordHandler(IWorkItemRepository workItemRepository, ICurrentUser currentUser)
    {
        _workItemRepository = workItemRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(UpdateTimeWeatherRecordCommand request, CancellationToken cancellationToken)
    {
        var workItem = await _workItemRepository.GetWorkItemWithRecordsByIdAsync(request.WorkItemId, cancellationToken);
        if (workItem == null || workItem.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(AppError.WorkItemNotFound(request.WorkItemId));
        }
    }
}