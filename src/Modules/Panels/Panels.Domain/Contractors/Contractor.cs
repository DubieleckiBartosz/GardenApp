namespace Panels.Domain.Contractors;

public class Contractor : Entity, IAggregateRoot
{
    private readonly List<Project> _projects;
    public string BusinessUserId { get; }
    public string Name { get; }
    public Email Email { get; }
    public Phone? Phone { get; private set; }
    public LogoImage? Logo { get; private set; }

    private Contractor()
    {
        _projects = new();
    }

    private Contractor(string businessUser, Email email, string name, Phone? phone)
    {
        BusinessUserId = businessUser;
        Email = email;
        Phone = phone;
        Name = name;
        _projects = new();
        IncrementVersion();
    }

    public static Contractor CreateContractor(
        string businessUser,
        Email email,
        string name,
        Phone? phone)
        => new Contractor(businessUser, email, name, phone);

    public void AddNewProject(string description)
    {
        _projects.Add(Project.NewProject(this.Id, description));
        IncrementVersion();
    }

    public void RemoveProject(int projectId)
    {
        var project = _projects.FirstOrDefault(_ => _.Id == projectId);
        if (project == null)
        {
            throw new ProjectNotFoundException(projectId);
        }

        _projects.Remove(project);
        IncrementVersion();
    }
}