namespace Works.Domain.WorkItems.Entities;

public class TimeWeatherRecord : Entity
{
    public TimeLog TimeLog { get; private set; }
    public Weather Weather { get; private set; }

    private TimeWeatherRecord()
    {
    }

    internal TimeWeatherRecord(TimeLog timeLog, Weather weather) => (TimeLog, Weather) = (timeLog, weather);

    internal void Update(TimeLog timeLog, Weather weather) => (TimeLog, Weather) = (timeLog, weather);
}