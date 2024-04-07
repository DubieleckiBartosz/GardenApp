namespace Works.Infrastructure.Database.Domain;

internal class TimeWeatherRecordEntityTypeConfiguration : IEntityTypeConfiguration<TimeWeatherRecord>
{
    public void Configure(EntityTypeBuilder<TimeWeatherRecord> builder)
    {
        builder.ToTable("TimeWeatherRecords", "works");
        builder.HasKey(_ => _.Id);

        builder.OwnsOne(twr => twr.TimeLog, timeLog =>
        {
            timeLog.Property(tl => tl.Minutes).HasColumnName("TimeLogMinutes").IsRequired();
            timeLog.Property(tl => tl.StartDate).HasColumnName("StartDate").IsRequired();
            timeLog.Property(tl => tl.EndDate).HasColumnName("EndDate").IsRequired();
            timeLog.Property(tl => tl.Created).HasColumnName("TimeLogCreated").IsRequired();
        });

        builder.HasOne<WorkItem>().WithMany(_ => _.TimeWeatherRecords).IsRequired();

        builder
          .Property<string>("_weathers")
          .HasColumnName("Weathers")
          .IsRequired(true);

        builder.Ignore(_ => _.Weathers);
    }
}