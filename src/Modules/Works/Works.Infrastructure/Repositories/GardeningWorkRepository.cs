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

    public async Task<GardeningWork?> GetGardeningWorkByIdAsync(int gardeningWorkId, CancellationToken cancellationToken = default)
    {
        return await _gardeningWorks.FirstOrDefaultAsync(_ => _.Id == gardeningWorkId, cancellationToken);
    }

    public async Task AddAsync(GardeningWork gardeningWork, CancellationToken cancellationToken = default)
    {
        await _gardeningWorks.AddAsync(gardeningWork, cancellationToken);
    }

    public void Update(GardeningWork gardeningWork)
    {
        _gardeningWorks.Update(gardeningWork);
    }
}