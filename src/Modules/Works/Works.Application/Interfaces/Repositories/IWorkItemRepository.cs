using Works.Domain.WorkItems;

namespace Works.Application.Interfaces.Repositories;

public interface IWorkItemRepository
{
    Task<WorkItem?> GetWorkItemByIdAsync(int workItemId);

    Task<WorkItem?> GetWorkItemWithRecordsByIdAsync(int workItemId);

    Task AddAsync(WorkItem item);

    void Update(WorkItem item);
}