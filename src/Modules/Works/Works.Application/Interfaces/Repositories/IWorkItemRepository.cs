namespace Works.Application.Interfaces.Repositories;

public interface IWorkItemRepository : IRepository<WorkItem>
{
    Task<WorkItem?> GetWorkItemByIdAsync(int workItemId, CancellationToken cancellationToken = default);

    Task<WorkItem?> GetWorkItemWithRecordsByIdAsync(int workItemId, CancellationToken cancellationToken = default);

    Task AddAsync(WorkItem item, CancellationToken cancellationToken = default);

    void Update(WorkItem item);
}