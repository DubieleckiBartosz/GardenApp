namespace BuildingBlocks.Infrastructure.Database.EntityFramework.Extensions;

public static class DispatcherExtension
{
    public static async Task DispatchDomainEventsAsync(this IDomainDecorator decorator, DbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
      .Entries<Entity>()
      .Where(x => x.Entity.Events != null && x.Entity.Events.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Events)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        foreach (var domainEvent in domainEvents)
        {
            await decorator.Publish(domainEvent);
        }
    }
}