﻿namespace Panels.Domain.Contractors;

public class Contractor : Entity, IAggregateRoot
{
    private string _socialMediaLinks = string.Empty;
    private readonly List<Project> _projects;
    public string BusinessUserId { get; }
    public string Name { get; }
    public Email Email { get; }
    public Phone? Phone { get; private set; }
    public LogoImage? Logo { get; private set; }

    public LinkList Links
    {
        get { return (LinkList)_socialMediaLinks; }
        set { _socialMediaLinks = value; }
    }

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

    public void UpdateProjectDescription(int projectId, string description)
    {
        var project = _projects.SingleOrDefault(_ => _.Id == projectId);
        if (project == null)
        {
            throw new ProjectNotFoundException(projectId);
        }

        project.UpdateDescription(description);
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

    public void AddLogo(LogoImage logoImage)
    {
        Logo = logoImage;
        IncrementVersion();
    }

    public void RemoveLogo()
    {
        if (Logo == null)
        {
            throw new LogoNotFoundException(this.Id);
        }

        Logo = null;
        IncrementVersion();
    }

    public void AddLink(SocialMediaLink link)
    {
        _socialMediaLinks = Links.AddLink(link);
        IncrementVersion();
    }

    public void RemoveLink(LinkType linkType)
    {
        var link = Links.LinkByType(linkType)
            ?? throw new LinkNotFoundException(linkType, this.Id);

        _socialMediaLinks = Links.RemoveLink(link);
        IncrementVersion();
    }
}