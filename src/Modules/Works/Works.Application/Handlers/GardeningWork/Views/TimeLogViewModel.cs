namespace Works.Application.Handlers.GardeningWork.Views;

public class TimeLogViewModel
{
    public int Id { get; }
    public short Minutes { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public DateTime Created { get; }

    private TimeLogViewModel(int id, short minutes, DateTime startDate, DateTime endDate, DateTime created)
    {
        Id = id;
        Minutes = minutes;
        StartDate = startDate;
        EndDate = endDate;
        Created = created;
    }

    public static explicit operator TimeLogViewModel(TimeLogDao logDao) => new(logDao.Id, logDao.Minutes, logDao.StartDate, logDao.EndDate, logDao.Created);
}