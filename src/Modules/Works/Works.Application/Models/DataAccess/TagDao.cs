namespace Works.Application.Models.DataAccess;

public class TagDao
{
    public string Value { get; set; }
    public string Bg { get; set; }
    public string Text { get; set; }

    public TagDao(string value, string bg, string text)
    {
        Value = value;
        Bg = bg;
        Text = text;
    }

    public static explicit operator TagDao(string tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
        {
            throw new ArgumentException("Invalid string format", nameof(tag));
        }

        var values = tag.Split('|');
        return new TagDao(values[0], values[1], values[2]);
    }
}