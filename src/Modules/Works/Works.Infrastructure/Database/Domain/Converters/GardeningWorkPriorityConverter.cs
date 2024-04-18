namespace Works.Infrastructure.Database.Domain.Converters;

internal class GardeningWorkPriorityConverter : ValueConverter<GardeningWorkPriority, int>
{
    public GardeningWorkPriorityConverter() : base(v => v.Id,
        v => Enumeration.GetById<GardeningWorkPriority>(v))
    {
    }
}