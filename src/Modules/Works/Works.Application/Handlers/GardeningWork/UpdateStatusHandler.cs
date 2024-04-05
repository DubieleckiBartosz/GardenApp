﻿namespace Works.Application.Handlers.GardeningWork;

public sealed class UpdateStatusHandler : ICommandHandler<GardeningWorkUpdateStatusCommand, Response>
{
    public record GardeningWorkUpdateStatusCommand(int GardeningWorkId, int NewStatus) : ICommand<Response>
    {
        public static GardeningWorkUpdateStatusCommand Create(GardeningWorkUpdateStatusParameters parameters)
            => new(parameters.GardeningWorkId, parameters.NewStatus);
    }

    public Task<Response> Handle(GardeningWorkUpdateStatusCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}