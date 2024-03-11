namespace Users.Infrastructure.Database.Domain;

public class ApplicationUserTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Ignore(_ => _.PhoneNumberConfirmed);

        builder.Property(_ => _.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(_ => _.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(_ => _.City).HasColumnName("City").IsRequired();
    }
}