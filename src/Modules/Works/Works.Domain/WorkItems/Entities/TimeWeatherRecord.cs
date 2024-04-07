namespace Works.Domain.WorkItems.Entities;

public class TimeWeatherRecord : Entity
{
    private string _weathers = string.Empty;
    public TimeLog TimeLog { get; private set; }

    public WeatherList Weathers
    {
        get { return (WeatherList)_weathers; }
        set { _weathers = value; }
    }

    private TimeWeatherRecord()
    {
    }

    internal TimeWeatherRecord(TimeLog timeLog, List<Weather> weathers)
    {
        TimeLog = timeLog;
        _weathers = new WeatherList(weathers);
        Version++;
    }

    internal void Update(TimeLog timeLog, List<Weather> weathers)
    {
        TimeLog = timeLog;
        _weathers = new WeatherList(weathers);
        Version++;
    }
}