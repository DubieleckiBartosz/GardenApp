namespace Payments.Application.Interfaces.Services;

public interface IProcessService
{
    Task StatusProcess(Event @event);
}