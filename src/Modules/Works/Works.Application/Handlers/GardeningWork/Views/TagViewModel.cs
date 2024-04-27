namespace Works.Application.Handlers.GardeningWork.Views;

public class TagViewModel
{
    public string Value { get; }
    public string Bg { get; }
    public string Text { get; }

    private TagViewModel(string value, string bg, string text)
    {
        Value = value;
        Bg = bg;
        Text = text;
    }

    public static explicit operator TagViewModel(TagDao tagDao) => new(tagDao.Value, tagDao.Bg, tagDao.Text);
}