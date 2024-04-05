namespace Works.Infrastructure.Repositories;

internal class GardeningWorkRepository : IGardeningWorkRepository
{
    private readonly WorksContext _worksContext;

    public IUnitOfWork UnitOfWork => _worksContext;

    public GardeningWorkRepository(WorksContext worksContext)
    {
        _worksContext = worksContext;
    }
}