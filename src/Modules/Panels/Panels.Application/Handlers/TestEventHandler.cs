using BuildingBlocks.Application.Contracts.Integration;

namespace Panels.Application.Handlers;

internal class TestEventHandler : IEventHandler<TestIntegrationEvent>
{
    public Task Handle(TestIntegrationEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}