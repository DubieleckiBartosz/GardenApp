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
            timeLog.Property(tl => tl.Date).HasColumnName("TimeLogDate").IsRequired();
            timeLog.Property(tl => tl.Created).HasColumnName("TimeLogCreated").IsRequired();
        });

        builder.OwnsOne(twr => twr.Weather, weather =>
        {
            weather.Property(w => w.Date).HasColumnName("WeatherDate").IsRequired();
            weather.Property(w => w.Clouds).HasColumnName("WeatherClouds").IsRequired();
            weather.Property(w => w.TemperatureC).HasColumnName("WeatherTemperatureC").IsRequired();
            weather.Property(w => w.Summary).HasColumnName("WeatherSummary").IsRequired();
            weather.Property(w => w.Wind).HasColumnName("WeatherWind").IsRequired();
        });
    }
}