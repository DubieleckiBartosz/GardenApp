using BuildingBlocks.Application.Attributes;

namespace BuildingBlocks.Application.Tools;

public static class BaseSerialize
{
    public static string Serialize<T>(this T data, JsonSerializerSettings? settings = null)
    {
        var json = JsonConvert.SerializeObject(data, settings);
        return json;
    }

    public static T DeserializeString<T>(this string json, Type type, JsonSerializerSettings? settings = null)
    {
        var result = JsonConvert.DeserializeObject(json, type, settings);
        return (T)result!;
    }

    public static IEvent? DeserializeQueueEvent(this string queueData, Assembly assembly, string queueKey)
    {
        var type = assembly.GetTypes().Where(_ =>
        {
            var attribute = Attribute.GetCustomAttribute(_, typeof(EventQueueAttribute));
            if (attribute == null)
            {
                return false;
            }

            if (!_.GetInterfaces().Contains(typeof(IEvent)))
            {
                return false;
            }

            var attr = (EventQueueAttribute)attribute;
            var valueRoutingKey = attr?.RoutingKey;
            return valueRoutingKey != null && valueRoutingKey.Equals(queueKey);
        }).Single();

        var data = JsonConvert.DeserializeObject(queueData, type, new JsonSerializerSettings
        {
            ContractResolver = new PrivateResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        });

        return data as IEvent;
    }
}