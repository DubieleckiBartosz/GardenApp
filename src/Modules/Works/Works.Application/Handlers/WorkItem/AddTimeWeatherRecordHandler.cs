namespace Works.Application.Handlers.WorkItem;

public sealed class AddTimeWeatherRecordHandler : ICommandHandler<AddTimeWeatherRecordCommand, AddTimeWeatherRecordResponse>
{
    public record AddTimeWeatherRecordCommand() : ICommand<AddTimeWeatherRecordResponse>
    {
        public static AddTimeWeatherRecordCommand Create(AddTimeWeatherRecordParameters parameters)
            => new();
    }

    public class AddTimeWeatherRecordResponse
    {
        public int TimeWeatherRecordId { get; }

        public AddTimeWeatherRecordResponse(int recordId)
        {
            TimeWeatherRecordId = recordId;
        }
    }

    public Task<AddTimeWeatherRecordResponse> Handle(AddTimeWeatherRecordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}