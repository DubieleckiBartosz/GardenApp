namespace Offers.Domain.Events;
public record OfferCompleted(string Recipient, string CreatorName, string TotalPrice) : IDomainEvent;