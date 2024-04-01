namespace Panels.Infrastructure.Database.Domain;

internal class ContractorEntityTypeConfiguration : IEntityTypeConfiguration<Contractor>
{
    public void Configure(EntityTypeBuilder<Contractor> builder)
    {
        builder.ToTable("Contractors");
        builder.HasKey(k => k.Id);

        builder.Property(_ => _.BusinessUserId)
            .HasColumnName("BusinessUserId")
            .IsRequired();

        builder.Property(_ => _.Name)
            .HasColumnName("Name")
            .IsRequired();

        builder.Property(_ => _.Email)
            .HasColumnName("Email")
            .HasConversion(x => x.Value, x => new Email(x))
            .IsRequired();

        builder.Property(_ => _.Phone)
            .HasColumnName("Phone")
            .HasConversion(x => x.Value, x => new Phone(x))
            .IsRequired(false)
            .HasDefaultValue(null);

        builder
            .Property(_ => _.Logo)
            .HasColumnName("LogoKey")
            .HasConversion(x => x.Key, x => new LogoImage(x))
            .IsRequired(false)
            .HasDefaultValue(null);

        builder
            .Property<string>("_socialMediaLinks")
            .HasColumnName("SocialMediaLinks")
            .IsRequired(false);

        builder.Ignore(_ => _.Links);

        builder.Property<DateTime>("Created").HasColumnName("Created").IsRequired();
        builder.Property<DateTime>("LastModified").HasColumnName("LastModified").IsRequired();
    }
}