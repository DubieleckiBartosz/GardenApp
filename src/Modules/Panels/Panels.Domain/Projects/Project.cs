using static System.Net.Mime.MediaTypeNames;

namespace Panels.Domain.Projects;

public class Project : Entity, IAggregateRoot
{
    private bool _isRemoved;

    private readonly List<ProjectImage> _images;
    public int ContractorId { get; }
    public string BusinessId { get; }
    public Date Created { get; }
    public string Description { get; private set; }
    public IEnumerable<ProjectImage> Images => _images;

    private Project()
    {
        _images = new();
    }

    private Project(int contractorId, string description, string businessId)
    {
        ContractorId = contractorId;
        BusinessId = businessId;
        Description = description;
        Created = Clock.CurrentDate();
        _images = new();
        _isRemoved = false;
        IncrementVersion();
    }

    internal static Project NewProject(
        int contractorId,
        string description,
        string businessId) => new(contractorId, description, businessId);

    public void UpdateDescription(string newDescription)
    {
        Description = newDescription;
        IncrementVersion();
    }

    public void AddImage(string key)
    {
        var image = _images.FirstOrDefault(_ => _.Key == key);
        if (image != null)
        {
            throw new UniqueImageKeyException(key);
        }

        _images.Add(ProjectImage.CreateNew(Id, key));
        IncrementVersion();
    }

    public void RemoveImage(string key)
    {
        var image = _images.FirstOrDefault(_ => _.Key == key);
        if (image == null)
        {
            throw new ImageNotFoundException(key);
        }

        _images.Remove(image);
        IncrementVersion();
    }

    public void Remove()
    {
        var images = _images?.Select(_ => _.Key);
        _images?.Clear();
        _isRemoved = true;

        if (images != null)
        {
            this.AddEvent(new ProjectRemoved(images));
        }
    }
}