namespace Works.Infrastructure.Database.Domain;

internal class WorkItemEntityTypeConfiguration : IEntityTypeConfiguration<WorkItem>
{
    public void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        builder.ToTable("WorkItems", "works");
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.GardeningWorkId).HasColumnName("GardeningWorkId");

        builder
            .HasOne<GardeningWork>()
            .WithMany()
            .HasForeignKey(_ => _.GardeningWorkId);

        builder.HasMany(_ => _.TimeWeatherRecords).WithOne();

        builder.Property(p => p.Status)
          .HasColumnName("Status")
          .HasColumnType("SMALLINT")
          .HasConversion<WorkItemStatusConverter>()
          .IsRequired();

        builder.Property(_ => _.Name)
           .IsRequired()
           .HasMaxLength(200);

        builder.Property(_ => _.EstimatedTimeInMinutes)
          .IsRequired(false)
          .HasDefaultValue(null);

        builder.Property(_ => _.RealTimeInMinutes)
          .IsRequired(false)
          .HasDefaultValue(null);
    }
}