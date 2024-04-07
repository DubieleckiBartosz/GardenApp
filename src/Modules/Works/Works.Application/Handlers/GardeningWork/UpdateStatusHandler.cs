namespace Works.Application.Handlers.GardeningWork;

public sealed class UpdateStatusHandler : ICommandHandler<GardeningWorkUpdateStatusCommand, Response>
{
    public record GardeningWorkUpdateStatusCommand(int GardeningWorkId, int NewStatus) : ICommand<Response>
    {
        public static GardeningWorkUpdateStatusCommand Create(GardeningWorkUpdateStatusParameters parameters)
            => new(parameters.GardeningWorkId, parameters.NewStatus);
    }

    private readonly IGardeningWorkRepository _gardeningWorkRepository;
    private readonly ICurrentUser _currentUser;

    public UpdateStatusHandler(IGardeningWorkRepository gardeningWorkRepository, ICurrentUser currentUser)
    {
        _gardeningWorkRepository = gardeningWorkRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(GardeningWorkUpdateStatusCommand request, CancellationToken cancellationToken)
    {
        var gardeningWork = await _gardeningWorkRepository.GetGardeningWorkByIdAsync(request.GardeningWorkId, cancellationToken);
        if (gardeningWork == null || gardeningWork.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(AppError.GardeningWorkNotFound(request.GardeningWorkId));
        }

        var status = Enumeration.GetById<GardeningWorkStatus>(request.NewStatus);
        gardeningWork.UpdateStatus(status);
        await _gardeningWorkRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}