namespace Works.Application.Handlers.GardeningWork;

public sealed class UpdatePlannedStartDateHandler : ICommandHandler<UpdatePlannedStartDateCommand, Response>
{
    public record UpdatePlannedStartDateCommand(int GardeningWorkId, DateTime NewPlannedStartDate) : ICommand<Response>
    {
        public static UpdatePlannedStartDateCommand Create(UpdatePlannedStartDateParameters parameters)
            => new(parameters.GardeningWorkId, parameters.NewPlannedStartDate);
    }

    private readonly IGardeningWorkRepository _gardeningWorkRepository;
    private readonly ICurrentUser _currentUser;

    public UpdatePlannedStartDateHandler(IGardeningWorkRepository gardeningWorkRepository, ICurrentUser currentUser)
    {
        _gardeningWorkRepository = gardeningWorkRepository;
        _currentUser = currentUser;
    }

    public Task<Response> Handle(UpdatePlannedStartDateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}