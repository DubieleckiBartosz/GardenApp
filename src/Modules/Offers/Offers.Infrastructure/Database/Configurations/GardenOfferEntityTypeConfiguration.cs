namespace Offers.Infrastructure.Database.Configurations;

internal class GardenOfferEntityTypeConfiguration : IEntityTypeConfiguration<GardenOffer>
{
    internal const string OfferItems = "_offerItems";

    public void Configure(EntityTypeBuilder<GardenOffer> builder)
    {
        builder.ToTable("GardenOffers");
        builder.HasKey(x => x.Id);

        builder.Property(_ => _.CreatorId).HasColumnName("CreatorId").IsRequired();
        builder.Property(_ => _.CreatorName).HasColumnName("CreatorName").IsRequired();
        builder.Property(_ => _.Description).HasColumnName("Description").IsRequired();
        builder.Property(_ => _.TotalPrice).HasColumnName("TotalPrice").IsRequired();
        builder.Property(_ => _.Recipient).HasColumnName("Recipient").IsRequired();

        builder.Property(_ => _.ExpirationDate).HasColumnName("ExpirationDate")
            .HasConversion(x => x.Value, x => new Date(x))
            .IsRequired();

        builder.Property(_ => _.Status).HasColumnName("Status")
          .HasColumnType("SMALLINT")
          .HasConversion<GardenOfferStatusConverter>()
          .IsRequired();

        builder.OwnsMany<GardenOfferItem>(OfferItems, _ =>
        {
            _.WithOwner().HasForeignKey("GardenOfferId");
            _.ToTable("GardenOfferItems");
            _.HasKey(k => k.Id);

            _.Property(p => p.Code).HasColumnName("Code").IsRequired();
            _.Property(p => p.Name).HasColumnName("Name").IsRequired();
            _.Property(p => p.Price).HasColumnName("Price").IsRequired();

            _.Property<DateTime>("Created").HasColumnName("Created").IsRequired();
            _.Property<DateTime>("LastModified").HasColumnName("LastModified").IsRequired();
        });

        builder.Property<DateTime>("Created").HasColumnName("Created").IsRequired();
        builder.Property<DateTime>("LastModified").HasColumnName("LastModified").IsRequired();

        builder.Ignore(_ => _.IsExpired);
    }
}