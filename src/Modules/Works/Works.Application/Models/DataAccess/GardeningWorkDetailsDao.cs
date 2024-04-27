namespace Works.Application.Models.DataAccess;

public class GardeningWorkDetailsDao : GardeningWorkDao
{
    public IEnumerable<WorkItemDao> Items { get; set; }
}