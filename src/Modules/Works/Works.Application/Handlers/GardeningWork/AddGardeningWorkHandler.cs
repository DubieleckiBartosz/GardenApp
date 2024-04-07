namespace Works.Application.Handlers.GardeningWork;

public sealed class AddGardeningWorkHandler : ICommandHandler<AddGardeningWorkCommand, Response<AddGardeningWorkResponse>>
{
    public record AddGardeningWorkCommand(
        string ClientEmail,
        DateTime PlannedStartDate,
        DateTime? RealStartDate,
        DateTime? PlannedEndDate,
        DateTime? RealEndDate,
        string City,
        string Street,
        string NumberStreet) : ICommand<Response<AddGardeningWorkResponse>>
    {
        public static AddGardeningWorkCommand Create(AddGardeningWorkParameters parameters)
            => new(
                parameters.ClientEmail,
                parameters.PlannedStartDate,
                parameters.RealStartDate,
                parameters.PlannedEndDate,
                parameters.RealEndDate,
                parameters.City,
                parameters.Street,
                parameters.NumberStreet);
    }

    public class AddGardeningWorkResponse
    {
        public int GardeningWorkId { get; }

        public AddGardeningWorkResponse(int gardeningWorkId)
        {
            GardeningWorkId = gardeningWorkId;
        }
    }

    private readonly IGardeningWorkRepository _gardeningWorkRepository;
    private readonly ICurrentUser _currentUser;

    public AddGardeningWorkHandler(IGardeningWorkRepository gardeningWorkRepository, ICurrentUser currentUser)
    {
        _gardeningWorkRepository = gardeningWorkRepository;
        _currentUser = currentUser;
    }

    public async Task<Response<AddGardeningWorkResponse>> Handle(AddGardeningWorkCommand request, CancellationToken cancellationToken)
    {
        var location = new Location(request.City, request.Street, request.NumberStreet);

        var newGardeningWork = Domain.GardeningWorks.GardeningWork.Create(
            _currentUser.UserId, request.ClientEmail, request.PlannedStartDate,
            request.RealStartDate, request.PlannedEndDate, request.RealEndDate, location);

        await _gardeningWorkRepository.AddAsync(newGardeningWork, cancellationToken);
        await _gardeningWorkRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response<AddGardeningWorkResponse>.Ok(new AddGardeningWorkResponse(newGardeningWork.Id));
    }
}