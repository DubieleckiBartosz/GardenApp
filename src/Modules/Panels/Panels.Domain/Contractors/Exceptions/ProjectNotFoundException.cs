namespace Panels.Domain.Contractors.Exceptions;

internal class ProjectNotFoundException : BaseException
{
    internal ProjectNotFoundException(int projectId) : base($"Project not found. [ProjectId: {projectId}]")
    {
    }
}