namespace Works.Application.Handlers.GardeningWork;

public sealed class UpdatePlannedStartDateHandler : ICommandHandler<UpdatePlannedStartDateCommand, Response>
{
    public record UpdatePlannedStartDateCommand(int GardeningWorkId, DateTime NewPlannedStartDate) : ICommand<Response>
    {
        public static UpdatePlannedStartDateCommand Create(UpdatePlannedStartDateParameters parameters)
            => new(parameters.GardeningWorkId, parameters.NewPlannedStartDate);
    }

    public Task<Response> Handle(UpdatePlannedStartDateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}