namespace Works.Application.Interfaces.Repositories;

public interface IGardeningWorkRepositoryDao
{
    Task<List<GardeningWorkDao>> GetGardeningWorks(string businessId);

    Task<GardeningWorkDetailsDao> GetGardeningWorkDetails(int gardeningWorkId);
}