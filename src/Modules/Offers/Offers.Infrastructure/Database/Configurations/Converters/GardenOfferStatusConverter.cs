namespace Offers.Infrastructure.Database.Configurations.Converters;

internal class GardenOfferStatusConverter : ValueConverter<OfferStatus, int>
{
    public GardenOfferStatusConverter() : base(v => v.Id,
        v => Enumeration.GetById<OfferStatus>(v))
    {
    }
}