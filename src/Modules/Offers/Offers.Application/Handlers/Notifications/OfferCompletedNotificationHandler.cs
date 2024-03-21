namespace Offers.Application.Handlers.Notifications;

internal class OfferCompletedNotificationHandler : IDomainEventHandler<OfferCompleted>
{
    public Task Handle(DomainEvent<OfferCompleted> notification, CancellationToken cancellationToken)
    {
        //Email to recipient

        return Task.CompletedTask;
    }
}