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
    }
}