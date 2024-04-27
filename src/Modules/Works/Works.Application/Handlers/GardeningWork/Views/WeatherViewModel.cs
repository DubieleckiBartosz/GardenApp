namespace Works.Application.Handlers.GardeningWork.Views;

public class WeatherViewModel
{
    public int Clouds { get; }
    public DateTime Date { get; }
    public int TemperatureC { get; }
    public string Summary { get; }
    public decimal Wind { get; }

    private WeatherViewModel(int clouds, DateTime date, int temperatureC, string summary, decimal wind)
    {
        Clouds = clouds;
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
        Wind = wind;
    }

    public static explicit operator WeatherViewModel(WeatherDao weatherDao) => new(weatherDao.Clouds, weatherDao.Date, weatherDao.TemperatureC, weatherDao.Summary, weatherDao.Wind);
}