namespace Payments.Infrastructure.Database.Domain;

internal class TemplateTypeConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.ToTable("Templates", PaymentsContext.PaymentsSchema);

        builder.HasKey(b => b.Id);

        builder.Property(_ => _.Subject).HasColumnName("Subject").IsRequired();
        builder.Property(_ => _.Value).HasColumnName("Value").IsRequired();

        builder.Property<DateTime>("Created").HasColumnName("Created").IsRequired()
               .HasDefaultValueSql("CURRENT_TIMESTAMP"); ;
    }
}