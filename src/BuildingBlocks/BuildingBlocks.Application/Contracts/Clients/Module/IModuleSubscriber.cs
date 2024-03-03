namespace BuildingBlocks.Application.Contracts.Clients.Module;

public interface IModuleSubscriber
{
    IModuleSubscriber Subscribe<TRequest, TResponse>(string path,
        Func<TRequest, IServiceProvider, CancellationToken, Task<TResponse>> action)
        where TRequest : class where TResponse : class;
}