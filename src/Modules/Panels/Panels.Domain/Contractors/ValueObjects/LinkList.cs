namespace Panels.Domain.Contractors.ValueObjects;

//https://enterprisecraftsmanship.com/posts/representing-collection-as-value-object/
public class LinkList
{
    private List<SocialMediaLink> _links { get; }
    public List<SocialMediaLink> Values => _links;

    private LinkList()
    {
        _links = new();
    }

    public LinkList(List<SocialMediaLink> links)
    {
        _links = links;
    }

    public SocialMediaLink? LinkByType(LinkType type) => _links.FirstOrDefault(_ => _.Type == type);

    public LinkList AddLink(SocialMediaLink linkSocialMedia)
    {
        if (_links.Any(_ => _ == linkSocialMedia))
        {
            throw new LinkMustBeUniqueException();
        }

        var links = _links;

        links.Add(linkSocialMedia);

        return new LinkList(links);
    }

    public LinkList RemoveLink(SocialMediaLink linkSocialMedia)
    {
        var links = _links.Where(_ => _ != linkSocialMedia)?.ToList() ?? new();

        links.Remove(linkSocialMedia);

        return new LinkList(links);
    }

    public static explicit operator LinkList(string links)
    {
        if (string.IsNullOrEmpty(links))
        {
            return new();
        }

        List<SocialMediaLink> data = links.Split(';')
            .Select(x => (SocialMediaLink)x)
            .ToList();

        return new LinkList(data);
    }

    public static implicit operator string(LinkList linkList)
    {
        return string.Join(";", linkList._links.Select(x => x.ToString()));
    }
}