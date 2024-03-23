namespace Users.Application.Interfaces;

public interface IPanelClient
{
    Task<ResponseClient> CreateNewContractorAsync(CreateNewContractorRequest request);
}