namespace Works.Application.Handlers.GardeningWork.Views;

public class TimeWeatherRecordViewModel
{
    public TimeLogViewModel TimeLog { get; }
    public IEnumerable<WeatherViewModel> Weathers { get; }

    private TimeWeatherRecordViewModel(TimeLogViewModel timeLog, IEnumerable<WeatherViewModel> weathers)
    {
        TimeLog = timeLog;
        Weathers = weathers;
    }

    public static explicit operator TimeWeatherRecordViewModel(TimeWeatherRecordDao record)
    {
        var log = (TimeLogViewModel)record.TimeLog;
        var weathers = record.Weathers.Select(_ => (WeatherViewModel)_);
        return new TimeWeatherRecordViewModel(log, weathers);
    }
}