namespace Works.Infrastructure.Database.Domain.Converters;

internal class WorkItemStatusConverter : ValueConverter<WorkItemStatus, int>
{
    public WorkItemStatusConverter() : base(v => v.Id,
        v => Enumeration.GetById<WorkItemStatus>(v))
    {
    }
}