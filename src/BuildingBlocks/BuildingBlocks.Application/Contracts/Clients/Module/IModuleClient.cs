namespace BuildingBlocks.Application.Contracts.Clients.Module;

public interface IModuleClient
{
    Task<TResult?> SendAsync<TResult>(string path, object request, CancellationToken cancellationToken = default) where TResult : class;
}