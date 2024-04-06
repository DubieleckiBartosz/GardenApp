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

    public Task<Response> Handle(UpdateTimeWeatherRecordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}