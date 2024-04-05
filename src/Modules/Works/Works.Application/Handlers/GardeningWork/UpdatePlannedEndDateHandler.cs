namespace Works.Application.Handlers.GardeningWork;

public sealed class UpdatePlannedEndDateHandler : ICommandHandler<UpdatePlannedEndDateCommand, Response>
{
    public record UpdatePlannedEndDateCommand(int GardeningWorkId, DateTime NewPlannedEndDate) : ICommand<Response>
    {
        public static UpdatePlannedEndDateCommand Create(UpdatePlannedEndDateParameters parameters)
            => new(parameters.GardeningWorkId, parameters.NewPlannedEndDate);
    }

    public Task<Response> Handle(UpdatePlannedEndDateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}