using BuildingBlocks.Application.Contracts.Repositories;

namespace Panels.Application.Interfaces;

public interface IContractorRepository : IRepository<Contractor>
{
    Task<Contractor?> GetByBusinessIdAsync(string businessId);

    Task<Contractor?> GetByBusinessIdNTAsync(string businessId);

    Task CreateNewContractorAsync(Contractor contractor);

    Task SaveChangesAsync();
}