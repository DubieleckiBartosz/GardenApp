namespace Panels.Infrastructure.Repositories;

internal class ProjectRepository : IProjectRepository
{
    private readonly PanelsContext _panelsContext;

    public IUnitOfWork UnitOfWork => _panelsContext;

    public ProjectRepository(PanelsContext panelsContext)
    {
        _panelsContext = panelsContext;
    }

    public async Task CreateNewProjectAsync(Project project)
    {
        await _panelsContext.AddAsync(project);
    }

    public async Task<Project?> GetByProjectIdAsync(int projectId)
    {
        return await _panelsContext.Projects.FirstOrDefaultAsync(_ => _.Id == projectId);
    }
}