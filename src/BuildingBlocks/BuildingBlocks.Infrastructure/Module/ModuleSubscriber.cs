namespace BuildingBlocks.Infrastructure.Module;

public class ModuleSubscriber : IModuleSubscriber
{
    private readonly IModuleActionRegistration _moduleRegistration;
    private readonly IServiceProvider _serviceProvider;

    public ModuleSubscriber(IModuleActionRegistration moduleRegistry, IServiceProvider serviceProvider)
    {
        _moduleRegistration = moduleRegistry;
        _serviceProvider = serviceProvider;
    }

    public IModuleSubscriber Subscribe<TRequest, TResponse>(string path,
        Func<TRequest, IServiceProvider, CancellationToken, Task<TResponse>> action)
        where TRequest : class where TResponse : class
    {
        _moduleRegistration.AddRequestAction(path, typeof(TRequest), typeof(TResponse),
            async (request, cancellationToken) =>
            {
                using var scope = _serviceProvider.CreateScope();
                return await action((TRequest)request, scope.ServiceProvider, cancellationToken);
            });

        return this;
    }
}