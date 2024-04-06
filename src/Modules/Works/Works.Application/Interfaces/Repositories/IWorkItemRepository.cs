namespace Works.Application.Interfaces.Repositories;

public interface IWorkItemRepository : IRepository<WorkItem>
{
    Task<WorkItem?> GetWorkItemByIdAsync(int workItemId);

    Task<WorkItem?> GetWorkItemWithRecordsByIdAsync(int workItemId);

    Task AddAsync(WorkItem item);

    void Update(WorkItem item);
}