namespace Works.Application.Handlers.GardeningWork.Views;

public class GardeningWorkTagViewModel
{
    public string Value { get; }
    public string Bg { get; }
    public string Text { get; }

    public GardeningWorkTagViewModel(string value, string bg, string text)
    {
        Value = value;
        Bg = bg;
        Text = text;
    }

    public static implicit operator GardeningWorkTagViewModel(Tag tag)
    {
        return new GardeningWorkTagViewModel(tag.Value, tag.Bg, tag.Text);
    }
}