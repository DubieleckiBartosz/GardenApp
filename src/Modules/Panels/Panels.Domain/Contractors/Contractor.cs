namespace Panels.Domain.Contractors;

public class Contractor : Entity, IAggregateRoot
{
    private string _socialMediaLinks = string.Empty;
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
    }

    private Contractor(string businessUser, Email email, string name, Phone? phone)
    {
        BusinessUserId = businessUser;
        Email = email;
        Phone = phone;
        Name = name;
        IncrementVersion();
    }

    public static Contractor CreateContractor(
        string businessUser,
        Email email,
        string name,
        Phone? phone)
        => new Contractor(businessUser, email, name, phone);

    public Project AddNewProject(string description) => Project.NewProject(this.Id, description, this.BusinessUserId);

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

        var key = Logo.Key;
        Logo = null;
        this.AddEvent(new LogoRemoved(key));
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