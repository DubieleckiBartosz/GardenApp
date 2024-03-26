namespace Panels.Application.Interfaces;

public interface IContractorRepository
{
    Task<Contractor?> GetByBusinessIdAsync(string businessId);

    Task<Contractor?> GetByBusinessIdNTAsync(string businessId);

    Task CreateNewContractorAsync(Contractor contractor);

    Task SaveChangesAsync();
}