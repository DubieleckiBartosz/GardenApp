namespace Users.Infrastructure.Database.Domain;

internal class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Ignore(_ => _.PhoneNumberConfirmed);

        builder.Property(_ => _.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(_ => _.LastName).HasColumnName("LastName").IsRequired();

        builder.HasMany(_ => _.RefreshTokens).WithOne(x => x.User).HasForeignKey(_ => _.UserId);
    }
}