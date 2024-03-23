namespace Panels.Domain.Contractors.ValueObjects;

internal class ProjectImage : ValueObject
{
    public Guid ProjectId { get; }
    public string Key { get; }

    private ProjectImage(Guid projectId, string key)
    {
        ProjectId = projectId;
        Key = key;
    }

    public static ProjectImage CreateNew(Guid projectId, string key) => new ProjectImage(projectId, key);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return ProjectId;
        yield return Key;
    }
}