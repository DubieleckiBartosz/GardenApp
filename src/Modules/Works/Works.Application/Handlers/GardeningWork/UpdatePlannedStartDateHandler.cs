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

    public async Task<Response> Handle(UpdatePlannedStartDateCommand request, CancellationToken cancellationToken)
    {
        var gardeningWork = await _gardeningWorkRepository.GetGardeningWorkByIdAsync(request.GardeningWorkId, cancellationToken);
        if (gardeningWork == null || gardeningWork.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(AppError.GardeningWorkNotFound(request.GardeningWorkId));
        }

        gardeningWork.UpdatePlannedStartDate(request.NewPlannedStartDate);
        await _gardeningWorkRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}