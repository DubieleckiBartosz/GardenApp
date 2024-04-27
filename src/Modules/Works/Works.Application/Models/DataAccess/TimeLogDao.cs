namespace Works.Application.Models.DataAccess;

public class TimeLogDao
{
    public int Id { get; set; }
    public short Minutes { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Created { get; set; }
}