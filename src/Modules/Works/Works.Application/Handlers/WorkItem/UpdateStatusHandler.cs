namespace Works.Application.Handlers.WorkItem;

public sealed class UpdateStatusHandler : ICommandHandler<WorkItemUpdateStatusCommand, Response>
{
    public record WorkItemUpdateStatusCommand() : ICommand<Response>
    {
        public static WorkItemUpdateStatusCommand Create(WorkItemUpdateStatusParameters parameters)
            => new();
    }

    public Task<Response> Handle(WorkItemUpdateStatusCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}