namespace Payments.Application.Interfaces.Services;

public interface IProcessService
{
    Task Process(Event @event);
}