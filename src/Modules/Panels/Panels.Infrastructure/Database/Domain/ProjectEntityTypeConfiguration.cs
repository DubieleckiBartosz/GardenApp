namespace Panels.Infrastructure.Database.Domain;

internal class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    internal const string Images = "_images";

    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Created)
              .HasColumnName("Created")
              .HasConversion(x => x.Value, x => new Date(x))
              .IsRequired();

        builder.Property(p => p.Description).HasColumnName("Description").IsRequired();

        builder.Property(_ => _.BusinessId)
            .HasColumnName("BusinessId")
            .IsRequired();

        builder.Property<bool>("_isRemoved")
            .HasColumnName("IsRemoved")
        .HasDefaultValue(false);

        builder.HasOne<Contractor>()
            .WithMany()
            .HasForeignKey(_ => _.ContractorId);

        builder.OwnsMany<ProjectImage>(Images, x =>
        {
            x.WithOwner().HasForeignKey("ProjectId");
            x.ToTable("ProjectImages");

            x.Property<int>("Id");
            x.HasKey("Id");

            x.Property(p => p.Key).IsRequired();
        });

        builder.Ignore(_ => _.Images);
        builder.HasQueryFilter(_ => EF.Property<bool>(_, "_isRemoved") == false);
    }
}