using BuildingBlocks.Application.Decorators;
using Offers.Domain.Events;

namespace Offers.Application.Handlers.Notifications;

internal class OfferCompletedNotificationHandler : IDomainEventHandler<OfferCompleted>
{
    public Task Handle(DomainEvent<OfferCompleted> notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}