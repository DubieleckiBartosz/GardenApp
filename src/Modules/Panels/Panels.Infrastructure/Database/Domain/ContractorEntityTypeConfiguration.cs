using Panels.Domain.Contractors.Entities;

namespace Panels.Infrastructure.Database.Domain;

internal class ContractorEntityTypeConfiguration : IEntityTypeConfiguration<Contractor>
{
    internal const string Projects = "_projects";
    internal const string Images = "_images";

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

        builder.OwnsMany<Project>(Projects, _ =>
        {
            _.WithOwner().HasForeignKey("ContractorId");
            _.ToTable("Projects");
            _.HasKey(k => k.Id);

            _.Property(p => p.Created)
                .HasColumnName("Created")
                .HasConversion(x => x.Value, x => new Date(x))
                .IsRequired();

            _.Property(p => p.Description).HasColumnName("Description").IsRequired();

            _.OwnsMany<ProjectImage>(Images, x =>
            {
                x.WithOwner().HasForeignKey("ProjectId");
                x.ToTable("ProjectImages");

                x.Property<int>("Id");
                x.HasKey("Id");

                x.Property(p => p.Key).IsRequired();
            });
        });

        builder.Property<DateTime>("Created").HasColumnName("Created").IsRequired();
        builder.Property<DateTime>("LastModified").HasColumnName("LastModified").IsRequired();
    }
}