namespace BuildingBlocks.Infrastructure.Integration;

public abstract class IntegrationEvent : IEvent
{
    public Guid Id { get; }

    public DateTime OccurredOn { get; }

    protected IntegrationEvent(Guid id, DateTime occurredOn)
    {
        this.Id = id;
        this.OccurredOn = occurredOn;
    }
}