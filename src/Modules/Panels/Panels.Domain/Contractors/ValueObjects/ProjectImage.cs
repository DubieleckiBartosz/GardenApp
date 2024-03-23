namespace Panels.Domain.Contractors.ValueObjects;

internal class ProjectImage : ValueObject
{
    public int ProjectId { get; }
    public string Key { get; }

    private ProjectImage(int projectId, string key)
    {
        ProjectId = projectId;
        Key = key;
    }

    public static ProjectImage CreateNew(int projectId, string key) => new ProjectImage(projectId, key);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return ProjectId;
        yield return Key;
    }
}