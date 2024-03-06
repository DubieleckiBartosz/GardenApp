namespace BuildingBlocks.Application.Tools;

public static class BaseSerialize
{
    public static string Serialize<T>(this T data, JsonSerializerSettings? settings = null)
    {
        var json = JsonConvert.SerializeObject(data, settings);
        return json;
    }

    public static T? DeserializeString<T>(this string? json, Type type, JsonSerializerSettings? settings = null)
    {
        if (json == null)
        {
            return default(T?);
        }

        var result = JsonConvert.DeserializeObject(json, type, settings);
        return (T)result!;
    }
}