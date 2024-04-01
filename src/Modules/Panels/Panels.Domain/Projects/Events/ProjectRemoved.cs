namespace Panels.Domain.Projects.Events;

public record class ProjectRemoved(IEnumerable<string> Images) : IDomainEvent;