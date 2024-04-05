namespace Works.Infrastructure.Repositories;

internal class GardeningWorkRepository : IGardeningWorkRepository
{
    private readonly WorksContext _worksContext;
    private readonly DbSet<GardeningWork> _gardeningWorks;
    public IUnitOfWork UnitOfWork => _worksContext;

    public GardeningWorkRepository(WorksContext worksContext)
    {
        _worksContext = worksContext;
        _gardeningWorks = worksContext.GardeningWorks;
    }

    public async Task<GardeningWork?> GetGardeningWorkByIdAsync(int gardeningWorkId)
    {
        return await _gardeningWorks.FirstOrDefaultAsync(_ => _.Id == gardeningWorkId);
    }

    public async Task AddAsync(GardeningWork gardeningWork)
    {
        await _gardeningWorks.AddAsync(gardeningWork);
    }

    public void Update(GardeningWork gardeningWork)
    {
        _gardeningWorks.Update(gardeningWork);
    }
}