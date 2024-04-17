namespace Works.Domain.GardeningWorks.ValueObjects;

public class TagList
{
    private List<Tag> _tags;
    public List<Tag> Values => _tags;

    private TagList()
    {
        _tags = new();
    }

    public TagList(List<Tag> tags)
    {
        _tags = tags;
    }

    public static explicit operator TagList(string tags)
    {
        if (string.IsNullOrEmpty(tags))
        {
            return new();
        }

        List<Tag> data = tags.Split(';')
            .Select(x => (Tag)x)
            .ToList();

        return new TagList(data);
    }

    public static implicit operator string(TagList tagList)
    {
        return string.Join(";", tagList._tags.Select(x => x.ToString()));
    }
}