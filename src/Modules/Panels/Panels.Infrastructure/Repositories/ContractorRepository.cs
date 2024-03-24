namespace Panels.Infrastructure.Repositories;

internal class ContractorRepository : IContractorRepository
{
    private readonly PanelsContext _panelsContext;

    public ContractorRepository(PanelsContext panelsContext)
    {
        _panelsContext = panelsContext;
    }

    public async Task CreateNewContractorAsync(Contractor contractor)
    {
        await _panelsContext.AddAsync(contractor);
    }

    public async Task<Contractor?> GetByBusinessIdNTAsync(string businessId)
    {
        return await _panelsContext.Contractors.FirstOrDefaultAsync(_ => _.BusinessUserId == businessId);
    }

    public async Task SaveChangesAsync()
    {
        await _panelsContext.SaveChangesAsync();
    }
}