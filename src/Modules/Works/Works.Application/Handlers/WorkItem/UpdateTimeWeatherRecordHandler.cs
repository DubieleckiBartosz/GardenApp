namespace Works.Application.Handlers.WorkItem;

public sealed class UpdateTimeWeatherRecordHandler : ICommandHandler<UpdateTimeWeatherRecordCommand, Response>
{
    public record UpdateTimeWeatherRecordCommand(int WorkItemId, int TimeWeatherRecordId, DateTime StartDate, DateTime EndDate) : ICommand<Response>
    {
        public static UpdateTimeWeatherRecordCommand Create(UpdateTimeWeatherRecordParameters parameters)
            => new(
                parameters.WorkItemId,
                parameters.TimeWeatherRecordId,
                parameters.StartDate,
                parameters.EndDate);
    }

    private readonly IWorkItemRepository _workItemRepository;
    private readonly IWeatherService _weatherService;
    private readonly ICurrentUser _currentUser;

    public UpdateTimeWeatherRecordHandler(IWorkItemRepository workItemRepository, IWeatherService weatherService, ICurrentUser currentUser)
    {
        _workItemRepository = workItemRepository;
        _weatherService = weatherService;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(UpdateTimeWeatherRecordCommand request, CancellationToken cancellationToken)
    {
        var workItem = await _workItemRepository.GetWorkItemWithRecordsByIdAsync(request.WorkItemId, cancellationToken);
        if (workItem == null || workItem.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(AppError.WorkItemNotFound(request.WorkItemId));
        }

        var timeLog = new TimeLog(request.StartDate.ToUTC(), request.EndDate.ToUTC());

        var weatherHistory = await _weatherService.GetHistoryAsync(new HistoryRequest());
        var weatherList = weatherHistory.List.Select(_ => _.Map()).ToList();

        workItem.UpdateTimeWeatherRecord(request.TimeWeatherRecordId, timeLog, weatherList);

        await _workItemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}