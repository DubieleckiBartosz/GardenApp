namespace Works.Application.Interfaces.Repositories;

public interface IGardeningWorkRepositoryDao
{
    Task<GardeningWorkDao> GetGardeningWorkDetails(int gardeningWorkId);
}