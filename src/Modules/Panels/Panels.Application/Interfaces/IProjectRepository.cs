using BuildingBlocks.Application.Contracts.Repositories;
using Panels.Domain.Projects;

namespace Panels.Application.Interfaces;

public interface IProjectRepository : IRepository<Project>
{
    Task CreateNewProjectAsync(Project project);

    Task<Project?> GetByProjectIdAsync(int projectId);
}