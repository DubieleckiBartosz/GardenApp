namespace Works.Application.Handlers.GardeningWork;

public sealed class UpdatePlannedEndDateHandler : ICommandHandler<UpdatePlannedEndDateCommand, Response>
{
    public record UpdatePlannedEndDateCommand(int GardeningWorkId, DateTime NewPlannedEndDate) : ICommand<Response>
    {
        public static UpdatePlannedEndDateCommand Create(UpdatePlannedEndDateParameters parameters)
            => new(parameters.GardeningWorkId, parameters.NewPlannedEndDate);
    }

    private readonly IGardeningWorkRepository _gardeningWorkRepository;
    private readonly ICurrentUser _currentUser;

    public UpdatePlannedEndDateHandler(IGardeningWorkRepository gardeningWorkRepository, ICurrentUser currentUser)
    {
        _gardeningWorkRepository = gardeningWorkRepository;
        _currentUser = currentUser;
    }

    public Task<Response> Handle(UpdatePlannedEndDateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}