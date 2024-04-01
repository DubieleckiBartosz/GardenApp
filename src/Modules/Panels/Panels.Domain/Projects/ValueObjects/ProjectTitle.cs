namespace Panels.Domain.Projects.ValueObjects;

internal class ProjectTitle : ValueObject
{
    public string Value { get; }

    public ProjectTitle(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value), "Title cannot be null or empty");
        }

        Value = value;
    }

    public static implicit operator ProjectTitle(string value) => new(value);

    public static implicit operator string(ProjectTitle projectTitle) => projectTitle.Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}