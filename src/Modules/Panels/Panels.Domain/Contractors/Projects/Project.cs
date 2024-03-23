namespace Panels.Domain.Contractors.Projects;

public class Project : Entity
{
    private readonly List<ProjectImage> _images;
    public int ContractorId { get; }
    public Date Created { get; }
    public string Description { get; private set; }

    private Project()
    {
        _images = new();
    }

    private Project(int contractorId, string description)
    {
        ContractorId = contractorId;
        Description = description;
        Created = Clock.CurrentDate();
        _images = new();
        IncrementVersion();
    }

    internal static Project NewProject(int contractorId, string description) => new(contractorId, description);

    internal void UpdateDescription(string newDescription)
    {
        Description = newDescription;
        IncrementVersion();
    }

    internal void AddImage(string key)
    {
        var image = _images.FirstOrDefault(_ => _.Key == key);
        if (image != null)
        {
            throw new UniqueImageKeyException(key);
        }

        _images.Add(ProjectImage.CreateNew(this.Id, key));
        IncrementVersion();
    }

    internal void RemoveImage(string key)
    {
        var image = _images.FirstOrDefault(_ => _.Key == key);
        if (image == null)
        {
            throw new ImageNotFoundException(key);
        }

        _images.Remove(image);
        IncrementVersion();
    }
}