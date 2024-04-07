namespace Works.Infrastructure.Repositories;

internal class WorkItemRepository : IWorkItemRepository
{
    private readonly WorksContext _worksContext;
    private readonly DbSet<WorkItem> _workItems;
    public IUnitOfWork UnitOfWork => _worksContext;

    public WorkItemRepository(WorksContext worksContext)
    {
        _worksContext = worksContext;
        _workItems = _worksContext.WorkItems;
    }

    public async Task<WorkItem?> GetWorkItemByIdAsync(int workItemId, CancellationToken cancellationToken = default)
    {
        return await _workItems.FirstOrDefaultAsync(_ => _.Id == workItemId, cancellationToken);
    }

    public async Task<WorkItem?> GetWorkItemWithRecordsByIdAsync(int workItemId, CancellationToken cancellationToken = default)
    {
        return await _workItems
            .Include(_ => _.TimeWeatherRecords)
            .FirstOrDefaultAsync(_ => _.Id == workItemId, cancellationToken);
    }

    public async Task AddAsync(WorkItem item, CancellationToken cancellationToken = default)
    {
        await _workItems.AddAsync(item, cancellationToken);
    }

    public void Update(WorkItem item)
    {
        _workItems.Update(item);
    }
}