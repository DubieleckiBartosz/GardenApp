namespace Works.Application.Handlers.GardeningWork.Views;

public class GardeningWorkDetailsViewModel : GardeningWorkViewModel
{
    public IEnumerable<WorkItemViewModel> WorkItems { get; }

    protected GardeningWorkDetailsViewModel(
        int id,
        string clientEmail,
        int priority,
        DateTime plannedStartDate,
        int status,
        string city,
        string street,
        string numberStreet,
        IEnumerable<GardeningWorkTagViewModel>? tags,
        IEnumerable<WorkItemViewModel> workItems) : base(id, clientEmail, priority, plannedStartDate, status, city, street, numberStreet, tags)
    {
        WorkItems = workItems;
    }
}