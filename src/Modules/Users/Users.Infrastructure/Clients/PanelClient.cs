namespace Users.Infrastructure.Clients;

internal class PanelClient : IPanelClient
{
    private readonly IModuleClient _client;

    public PanelClient(IModuleClient client)
    {
        _client = client;
    }

    public async Task<ResponseClient> CreateNewContractorAsync(CreateNewContractorRequest request)
    {
        var result = await _client.SendAsync<ResponseClient>("panel/create", request);
        return result ?? throw new InvalidOperationException("Response is null when calling the 'panel/create' module client");
    }
}