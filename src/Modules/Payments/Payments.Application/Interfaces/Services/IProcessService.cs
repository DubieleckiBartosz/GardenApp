namespace Payments.Application.Interfaces.Services;

internal interface IProcessService
{
    Task StatusProcess(Event @event);
}