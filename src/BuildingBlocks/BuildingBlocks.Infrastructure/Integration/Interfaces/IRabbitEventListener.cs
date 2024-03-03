namespace BuildingBlocks.Infrastructure.Integration.Interfaces;

internal interface IRabbitEventListener
{
    void Subscribe(Assembly assembly, Type type, string? queueName = null, string? routingKey = null);

    void Subscribe<TEvent>(Assembly assembly) where TEvent : IEvent;

    Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
}