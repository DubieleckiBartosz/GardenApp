namespace Users.Application.Interfaces;

public interface IPanelClient
{
    Task<ResponseClient<string>> CreateNewPanelAsync(CreateNewPanelRequest request);
}