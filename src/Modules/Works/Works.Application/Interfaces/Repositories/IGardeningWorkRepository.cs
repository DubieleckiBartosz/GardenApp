namespace Works.Application.Interfaces.Repositories;

public interface IGardeningWorkRepository : IRepository<GardeningWork>
{
    Task<GardeningWork?> GetGardeningWorkByIdAsync(int gardeningWorkId);

    Task AddAsync(GardeningWork gardeningWork);

    void Update(GardeningWork gardeningWork);
}