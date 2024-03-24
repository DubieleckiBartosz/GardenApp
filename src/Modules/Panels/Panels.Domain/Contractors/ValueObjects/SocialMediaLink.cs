namespace Panels.Domain.Contractors.ValueObjects;

public class SocialMediaLink : ValueObject
{
    public static Dictionary<LinkType, Func<string, SocialMediaLink>> LinkCreator => new()
    {
        {LinkType.Instagram, (_) => CreateInstagramLink(_) },
        {LinkType.Facebook, (_) => CreateFacebookLink(_) },
        {LinkType.Twitter, (_) => CreateTwitterLink(_) },
        {LinkType.Youtube, (_) => CreateYoutubeLink(_) },
    };

    public string Value { get; }
    public LinkType Type { get; }

    public SocialMediaLink(string value, LinkType type)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value), "Social media link cannot be null or empty");
        }

        (this.Type, this.Value) = (type, value);
    }

    public static SocialMediaLink CreateInstagramLink(string value) => new(value, LinkType.Instagram);

    public static SocialMediaLink CreateFacebookLink(string value) => new(value, LinkType.Facebook);

    public static SocialMediaLink CreateTwitterLink(string value) => new(value, LinkType.Twitter);

    public static SocialMediaLink CreateYoutubeLink(string value) => new(value, LinkType.Youtube);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return this.Type;
    }

    public static explicit operator SocialMediaLink(string link)
    {
        var values = link.Split('|').Select(_ => _)!.ToList();
        return LinkCreator[(LinkType)int.Parse(values[1])].Invoke(values[0]);
    }

    public override string ToString()
    {
        return $"{Value}|{(int)Type}";
    }
}