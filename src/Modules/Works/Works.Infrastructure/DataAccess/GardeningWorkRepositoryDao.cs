namespace Works.Infrastructure.DataAccess;

internal class GardeningWorkRepositoryDao : IGardeningWorkRepositoryDao
{
    public async Task<List<GardeningWorkDao>> GetGardeningWorks(string businessId)
    {
        throw new NotImplementedException();
    }

    public async Task<GardeningWorkDetailsDao> GetGardeningWorkDetails(int gardeningWorkId)
    {
        throw new NotImplementedException();
    }
}