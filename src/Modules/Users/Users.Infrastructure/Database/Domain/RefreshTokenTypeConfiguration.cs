namespace Users.Infrastructure.Database.Domain;

internal class RefreshTokenTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens", UsersContext.UsersSchema);

        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Value).HasColumnName("Value").IsRequired();
        builder.Property(_ => _.TokenExpirationDate).HasColumnName("TokenExpirationDate").IsRequired();
        builder.Property(_ => _.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(_ => _.Revoked).HasColumnName("Revoked").HasDefaultValue("false").IsRequired();
        builder.Property(_ => _.ReplacedByToken).HasColumnName("ReplacedByToken").IsRequired(false);

        builder
            .Ignore(_ => _.IsActive)
            .Ignore(_ => _.IsExpired);
    }
}