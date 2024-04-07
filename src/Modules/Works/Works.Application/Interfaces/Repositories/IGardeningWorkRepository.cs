namespace Works.Application.Interfaces.Repositories;

public interface IGardeningWorkRepository : IRepository<GardeningWork>
{
    Task<GardeningWork?> GetGardeningWorkByIdAsync(int gardeningWorkId, CancellationToken cancellationToken = default);

    Task AddAsync(GardeningWork gardeningWork, CancellationToken cancellationToken = default);

    void Update(GardeningWork gardeningWork);
}