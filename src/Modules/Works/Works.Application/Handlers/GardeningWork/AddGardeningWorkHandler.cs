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

    public async Task<Response<AddGardeningWorkResponse>> Handle(AddGardeningWorkCommand request, CancellationToken cancellationToken)
    {
        return Response<AddGardeningWorkResponse>.Ok(new AddGardeningWorkResponse(1));
    }
}