namespace Works.Application.Handlers.WorkItem;

public sealed class AddTimeWeatherRecordHandler : ICommandHandler<AddTimeWeatherRecordCommand, AddTimeWeatherRecordResponse>
{
    public record AddTimeWeatherRecordCommand(int WorkItemId, int Minutes, DateTime Date) : ICommand<AddTimeWeatherRecordResponse>
    {
        public static AddTimeWeatherRecordCommand Create(AddTimeWeatherRecordParameters parameters)
            => new(parameters.WorkItemId, parameters.Minutes, parameters.Date);
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
    private readonly ICurrentUser _currentUser;

    public AddTimeWeatherRecordHandler(IWorkItemRepository workItemRepository, ICurrentUser currentUser)
    {
        _workItemRepository = workItemRepository;
        _currentUser = currentUser;
    }

    public Task<AddTimeWeatherRecordResponse> Handle(AddTimeWeatherRecordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}