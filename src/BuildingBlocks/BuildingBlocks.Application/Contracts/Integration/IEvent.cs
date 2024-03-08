namespace BuildingBlocks.Application.Contracts.Integration;

public interface IEvent : INotification
{
    Guid Id { get; set; }

    DateTime OccurredOn { get; set; }
}