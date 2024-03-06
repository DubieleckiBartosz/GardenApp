using BuildingBlocks.Application.Contracts.Integration;
using BuildingBlocks.Application.Contracts.Mediator;
using BuildingBlocks.Domain.Abstractions;
using MediatR;
using static Users.Application.Integration.TestHandler;

namespace Users.Application.Integration;

internal class TestHandler : ICommandHandler<TestCommand, Unit>
{
    private record TestEvent(string TestProp) : IDomainEvent;
    public record TestCommand() : ICommand<Unit>;
    private readonly IEventDispatcher _eventDispatcher;

    public TestHandler(IEventDispatcher eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Unit> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        await _eventDispatcher.SendAsync(new TestEvent("test"));

        return Unit.Value;
    }
}