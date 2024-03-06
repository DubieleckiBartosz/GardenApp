using BuildingBlocks.Application.Attributes;

namespace Panels.Infrastructure.Integration.Events;

[IntegrationEventDecorator(IntegrationEventNavigators.TestNavigator)]
public class TestIntegrationEvent : IntegrationEvent
{
    public string TestProp { get; set; }

    protected TestIntegrationEvent(string testProp, Guid id, DateTime occurredOn) : base(id, occurredOn)
    {
        TestProp = testProp;
    }
}