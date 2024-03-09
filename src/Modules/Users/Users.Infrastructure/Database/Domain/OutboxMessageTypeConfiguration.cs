﻿namespace Users.Infrastructure.Database.Domain;

public class OutboxMessageTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages", "users");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedNever();
    }
}