namespace Works.Application.Handlers.WorkItem;

public sealed class UpdateTimeWeatherRecordHandler : ICommandHandler<UpdateTimeWeatherRecordCommand, Response>
{
    public record UpdateTimeWeatherRecordCommand() : ICommand<Response>
    {
        public static UpdateTimeWeatherRecordCommand Create(UpdateTimeWeatherRecordParameters parameters)
            => new();
    }

    public Task<Response> Handle(UpdateTimeWeatherRecordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}