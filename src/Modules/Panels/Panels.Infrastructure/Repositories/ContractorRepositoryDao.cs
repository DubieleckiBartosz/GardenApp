namespace Panels.Infrastructure.Repositories;

internal class ContractorRepositoryDao : BaseRepository, IContractorRepositoryDao
{
    public ContractorRepositoryDao(IConfiguration configuration, ILogger logger)
        : base(configuration["DapperConnectionString"]! + PanelsContext.PanelsSchema, logger)
    {
    }
}