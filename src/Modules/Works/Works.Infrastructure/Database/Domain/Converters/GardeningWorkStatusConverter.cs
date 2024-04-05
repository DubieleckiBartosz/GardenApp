namespace Works.Infrastructure.Database.Domain.Converters;

internal class GardeningWorkStatusConverter : ValueConverter<GardeningWorkStatus, int>
{
    public GardeningWorkStatusConverter() : base(v => v.Id,
        v => Enumeration.GetById<GardeningWorkStatus>(v))
    {
    }
}