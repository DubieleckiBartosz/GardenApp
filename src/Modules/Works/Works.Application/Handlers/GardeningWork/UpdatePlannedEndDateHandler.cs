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

    public async Task<Response> Handle(UpdatePlannedEndDateCommand request, CancellationToken cancellationToken)
    {
        var gardeningWork = await _gardeningWorkRepository.GetGardeningWorkByIdAsync(request.GardeningWorkId, cancellationToken);
        if (gardeningWork == null || gardeningWork.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(AppError.GardeningWorkNotFound(request.GardeningWorkId));
        }

        gardeningWork.UpdatePlannedEndDate(request.NewPlannedEndDate, cancellationToken);
        await _gardeningWorkRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}