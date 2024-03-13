namespace Users.Infrastructure.Database.Domain;

public class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Ignore(_ => _.PhoneNumberConfirmed);

        builder.Property(_ => _.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(_ => _.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(_ => _.City).HasColumnName("City").IsRequired();
    }
}