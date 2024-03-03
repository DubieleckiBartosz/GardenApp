namespace BuildingBlocks.Application.Contracts.Integration;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}