using BuildingBlocks.Application.Wrappers;
using BuildingBlocks.Infrastructure.Module;

namespace Users.Infrastructure.Clients;

internal class PanelClient : IPanelClient
{
    private readonly IModuleClient _client;

    public PanelClient(IModuleClient client)
    {
        _client = client;
    }

    public async Task<ResponseClient<string>> CreateNewPanelAsync(CreateNewPanelRequest request)
    {
        var result = await _client.SendAsync<ResponseClient<string>>("panel/create", request);
        return result ?? throw new InvalidOperationException("Response is null when calling the 'panel/create' module client");
    }
}