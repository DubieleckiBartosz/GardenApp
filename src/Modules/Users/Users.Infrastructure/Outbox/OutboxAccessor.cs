using Users.Infrastructure.Database;

namespace Users.Infrastructure.Outbox;

internal class OutboxAccessor : IOutboxListener
{
    private readonly UsersContext _usersContext;

    public OutboxAccessor(UsersContext usersContext)
    {
        _usersContext = usersContext;
    }

    public async Task AddAsync(OutboxMessage message)
    {
        await _usersContext.OutboxMessages.AddAsync(message);
    }
}