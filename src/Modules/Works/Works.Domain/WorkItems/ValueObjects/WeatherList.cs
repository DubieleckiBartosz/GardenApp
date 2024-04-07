namespace Works.Domain.WorkItems.ValueObjects;

public class WeatherList
{
    private List<Weather> _weathers;
    public List<Weather> Values => _weathers;

    private WeatherList()
    {
        _weathers = new();
    }

    public WeatherList(List<Weather> weathers)
    {
        _weathers = weathers;
    }

    public static explicit operator WeatherList(string weathers)
    {
        if (string.IsNullOrEmpty(weathers))
        {
            return new();
        }

        List<Weather> data = weathers.Split(';')
            .Select(x => (Weather)x)
            .ToList();

        return new WeatherList(data);
    }

    public static implicit operator string(WeatherList weatherList)
    {
        return string.Join(";", weatherList._weathers.Select(x => x.ToString()));
    }
}