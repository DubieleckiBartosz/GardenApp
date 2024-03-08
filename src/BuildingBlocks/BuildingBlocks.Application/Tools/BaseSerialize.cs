namespace BuildingBlocks.Application.Tools;

public static class BaseSerialize
{
    public static string Serialize<T>(this T data, JsonSerializerSettings? settings = null)
    {
        var json = JsonConvert.SerializeObject(data, settings);
        return json;
    }
}