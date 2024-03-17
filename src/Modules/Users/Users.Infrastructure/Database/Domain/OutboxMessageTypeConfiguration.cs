namespace Users.Infrastructure.Database.Domain;

internal class OutboxMessageTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages", UsersContext.UsersSchema);

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedNever();
    }
}