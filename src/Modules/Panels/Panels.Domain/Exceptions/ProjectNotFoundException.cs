namespace Panels.Domain.Exceptions;

internal class ProjectNotFoundException : BaseException
{
    internal ProjectNotFoundException(Guid projectId) : base($"Project not found. [ProjectId: {projectId}]")
    {
    }
}