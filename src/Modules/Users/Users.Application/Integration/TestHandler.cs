using BuildingBlocks.Application.Contracts.Integration;
using BuildingBlocks.Application.Contracts.Mediator;
using MediatR;
using static Users.Application.Integration.TestHandler;

namespace Users.Application.Integration;

public sealed class TestHandler : ICommandHandler<TestUsersCommand, Unit>
{
    public class TestIntegrationEvent : IntegrationEvent
    {
        public string TestProp { get; set; }

        public TestIntegrationEvent(string testProp) : base()
        {
            TestProp = testProp;
        }
    }

    public record TestUsersCommand() : ICommand<Unit>;
    private readonly IEventDispatcher _eventDispatcher;

    public TestHandler(IEventDispatcher eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Unit> Handle(TestUsersCommand request, CancellationToken cancellationToken)
    {
        await _eventDispatcher.SendAsync(new TestIntegrationEvent("test"));

        return Unit.Value;
    }
}