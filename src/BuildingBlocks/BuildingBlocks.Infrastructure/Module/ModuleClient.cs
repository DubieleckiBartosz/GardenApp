using BuildingBlocks.Application.Exceptions;

namespace BuildingBlocks.Infrastructure.Module;

public class ModuleClient : IModuleClient
{
    private readonly IModuleActionRegistration _moduleActionRegistration;

    public ModuleClient(IModuleActionRegistration moduleActionRegistration)
    {
        _moduleActionRegistration = moduleActionRegistration;
    }

    public async Task<TResult?> SendAsync<TResult>(string path, object request, CancellationToken cancellationToken = default) where TResult : class
    {
        var registration = _moduleActionRegistration.GetRequestRegistration(path);
        if (registration is null)
        {
            throw new InvalidOperationException($"No action has been defined for path: '{path}'.");
        }

        var parameters = GetClientRequiredModel(request, registration.RequestType);
        var result = await registration.Action(parameters, cancellationToken);

        if (result == null)
        {
            return null;
        }

        var value = JsonConvert.SerializeObject(result, JsonSettings.DefaultSerializerSettings);
        return JsonConvert.DeserializeObject<TResult>(value);
    }

    private object GetClientRequiredModel(object value, Type type)
    {
        try
        {
            string json = JsonConvert.SerializeObject(value, JsonSettings.DefaultSerializerSettings);
            return JsonConvert.DeserializeObject(json, type)!;
        }
        catch (JsonSerializationException ex)
        {
            throw new BadRequestException($"Error: {ex.Message}");
        }
    }
}