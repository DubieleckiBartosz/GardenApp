namespace BuildingBlocks.Application.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class EventQueueAttribute : Attribute
{
    public string? RoutingKey { get; init; }
    public string? QueueName { get; init; }

    public EventQueueAttribute()
    {
    }

    public EventQueueAttribute(string? queueName = null, string? routingKey = null)
    {
        QueueName = queueName;
        RoutingKey = routingKey;
    }
}