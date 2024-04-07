namespace Works.Application.Handlers.WorkItem;

public sealed class AddTimeWeatherRecordHandler : ICommandHandler<AddTimeWeatherRecordCommand, Response<AddTimeWeatherRecordResponse>>
{
    public record AddTimeWeatherRecordCommand(int WorkItemId, DateTime StartDate, DateTime EndDate) : ICommand<Response<AddTimeWeatherRecordResponse>>
    {
        public static AddTimeWeatherRecordCommand Create(AddTimeWeatherRecordParameters parameters)
            => new(parameters.WorkItemId, parameters.StartDate, parameters.EndDate);
    }

    public class AddTimeWeatherRecordResponse
    {
        public int TimeWeatherRecordId { get; }

        public AddTimeWeatherRecordResponse(int recordId)
        {
            TimeWeatherRecordId = recordId;
        }
    }

    private readonly IWorkItemRepository _workItemRepository;
    private readonly IWeatherService _weatherService;
    private readonly ICurrentUser _currentUser;

    public AddTimeWeatherRecordHandler(
        IWorkItemRepository workItemRepository,
        IWeatherService weatherService,
        ICurrentUser currentUser)
    {
        _workItemRepository = workItemRepository;
        _weatherService = weatherService;
        _currentUser = currentUser;
    }

    public async Task<Response<AddTimeWeatherRecordResponse>> Handle(AddTimeWeatherRecordCommand request, CancellationToken cancellationToken)
    {
        var workItem = await _workItemRepository.GetWorkItemWithRecordsByIdAsync(request.WorkItemId, cancellationToken);
        if (workItem == null || workItem.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(AppError.WorkItemNotFound(request.WorkItemId));
        }

        var timeLog = new TimeLog(request.StartDate.ToUTC(), request.EndDate.ToUTC());

        var weatherHistory = await _weatherService.GetHistoryAsync(new HistoryRequest());
        var weatherList = weatherHistory.List.Select(_ => _.Map()).ToList();
        var record = workItem.AddTimeWeatherRecord(timeLog, weatherList);

        await _workItemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response<AddTimeWeatherRecordResponse>.Ok(new AddTimeWeatherRecordResponse(record.Id));
    }
}