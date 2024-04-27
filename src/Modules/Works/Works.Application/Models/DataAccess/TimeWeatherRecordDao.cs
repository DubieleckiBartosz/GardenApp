namespace Works.Application.Models.DataAccess;

public class TimeWeatherRecordDao
{
    public string WeatherValue { get; set; }
    public TimeLogDao TimeLog { get; set; }
    public IEnumerable<WeatherDao> Weathers => WeatherValue.Split(';').Select(x => (WeatherDao)x);
}