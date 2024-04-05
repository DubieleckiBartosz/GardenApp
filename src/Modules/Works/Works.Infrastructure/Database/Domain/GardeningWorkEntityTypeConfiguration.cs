namespace Works.Infrastructure.Database.Domain;

internal class GardeningWorkEntityTypeConfiguration : IEntityTypeConfiguration<GardeningWork>
{
    public void Configure(EntityTypeBuilder<GardeningWork> builder)
    {
        builder.ToTable("GardeningWorks", "works");
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.ClientEmail)
          .HasColumnName("ClientEmail")
          .HasConversion(x => x.Value, x => new Email(x))
          .IsRequired(false)
          .HasDefaultValue(null);

        builder.Property(_ => _.PlannedStartDate)
          .HasColumnName("PlannedStartDate")
          .HasConversion(x => x.Value, x => new FutureDate(x))
          .IsRequired();

        builder.Property(_ => _.PlannedEndDate)
          .HasColumnName("PlannedEndDate")
          .HasConversion(x => x.Value, x => new FutureDate(x))
          .IsRequired(false)
          .HasDefaultValue(null);

        builder.Property(_ => _.RealStartDate)
            .IsRequired(false)
            .HasDefaultValue(null);

        builder.Property(_ => _.RealEndDate)
            .IsRequired(false)
            .HasDefaultValue(null);

        builder.Property(p => p.Status)
          .HasColumnName("Status")
          .HasColumnType("SMALLINT")
          .HasConversion<GardeningWorkStatusConverter>()
          .IsRequired();

        builder.OwnsOne(_ => _.Location, location =>
        {
            location.Property(l => l.City).HasColumnName("City").IsRequired();
            location.Property(l => l.Street).HasColumnName("Street").IsRequired();
            location.Property(l => l.NumberStreet).HasColumnName("NumberStreet").IsRequired();
        });
    }
}