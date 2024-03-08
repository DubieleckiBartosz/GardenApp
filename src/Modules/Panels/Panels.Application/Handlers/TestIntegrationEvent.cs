using BuildingBlocks.Application.Attributes;
using BuildingBlocks.Application.Contracts.Integration;
using Panels.Application.Constants;

namespace Panels.Application.Handlers;

[IntegrationEventDecorator(IntegrationEventNavigators.TestNavigator)]
public class TestIntegrationEvent : IntegrationEvent
{
    public string TestProp { get; set; }

    private TestIntegrationEvent()
    {
    }

    protected TestIntegrationEvent(string testProp, Guid id, DateTime occurredOn) : base(id, occurredOn)
    {
        TestProp = testProp;
    }
}