namespace BuildingBlocks.Application.Settings;

public class JsonSettings
{
    public static JsonSerializerSettings DefaultSerializerSettings => new()
    {
        ContractResolver = new PrivateResolver(),
        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        TypeNameHandling = TypeNameHandling.None
    };
}