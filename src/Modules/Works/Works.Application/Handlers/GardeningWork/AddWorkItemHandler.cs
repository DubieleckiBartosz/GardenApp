namespace Works.Application.Handlers.GardeningWork;

public sealed class AddWorkItemHandler : ICommandHandler<AddWorkItemCommand, Response<AddWorkItemResponse>>
{
    public record AddWorkItemCommand(int GardeningWorkId, string Name, int? EstimatedTimeInMinutes) : ICommand<Response<AddWorkItemResponse>>
    {
        public static AddWorkItemCommand Create(AddWorkItemParameters parameters)
            => new(parameters.GardeningWorkId, parameters.Name, parameters.EstimatedTimeInMinutes);
    }

    public class AddWorkItemResponse
    {
        public int WorkItemId { get; }

        public AddWorkItemResponse(int workId)
        {
            WorkItemId = workId;
        }
    }

    public async Task<Response<AddWorkItemResponse>> Handle(AddWorkItemCommand request, CancellationToken cancellationToken)
    {
        return Response<AddWorkItemResponse>.Ok(new AddWorkItemResponse(1));
    }
}