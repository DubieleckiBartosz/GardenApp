using BuildingBlocks.Domain.Time;

namespace BuildingBlocks.Application.Contracts.Integration;

public abstract class IntegrationEvent : IEvent
{
    public Guid Id { get; set; }

    public DateTime OccurredOn { get; set; }

    protected IntegrationEvent(Guid id, DateTime occurredOn)
    {
        this.Id = id;
        this.OccurredOn = occurredOn;
    }

    protected IntegrationEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = Clock.CurrentDate();
    }
}