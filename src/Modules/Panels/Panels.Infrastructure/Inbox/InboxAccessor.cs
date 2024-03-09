namespace Panels.Infrastructure.Inbox;

internal class InboxAccessor : IInboxAccessor
{
    private readonly DbSet<InboxMessage> _inboxMessages;
    private readonly PanelsContext _panelsContext;

    public InboxAccessor(PanelsContext panelsContext)
    {
        _panelsContext = panelsContext;
        _inboxMessages = panelsContext.InboxMessages;
    }

    public async Task AddAsync(InboxMessage message)
    {
        await _inboxMessages.AddAsync(message);
        await _panelsContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(IEnumerable<InboxMessage> messages)
    {
        _inboxMessages.RemoveRange(messages);
        await _panelsContext.SaveChangesAsync();
    }

    public async Task<InboxMessage?> GetMessageAsync(Guid id) => await _inboxMessages.FirstOrDefaultAsync(message => message.Id == id);

    public async Task<IEnumerable<Guid>> GetUnprocessedMessageIdsAsync() => await _inboxMessages.Where(_ => _.ProcessedDate == null).Select(message => message.Id).ToListAsync();

    public async Task SetMessageToProcessedAsync(Guid id)
    {
        await _inboxMessages
            .Where(_ => _.Id == id)
            .ExecuteUpdateAsync(_ => _.SetProperty(x => x.ProcessedDate, Clock.CurrentDate()));

        await _panelsContext.SaveChangesAsync();
    }
}