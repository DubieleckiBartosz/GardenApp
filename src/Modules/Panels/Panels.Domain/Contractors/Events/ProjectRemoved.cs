namespace Panels.Domain.Contractors.Events;

public record class ProjectRemoved(IEnumerable<string> Images) : IDomainEvent;