using BuildingBlocks.Application.Contracts.Integration;

namespace Users.Infrastructure.Integration;

internal class TestIntegrationEvent : IntegrationEvent
{
    public string TestProp { get; set; }

    protected TestIntegrationEvent(string testProp, Guid id, DateTime occurredOn) : base(id, occurredOn)
    {
        TestProp = testProp;
    }
}