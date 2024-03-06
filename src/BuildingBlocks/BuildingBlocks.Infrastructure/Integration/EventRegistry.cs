namespace BuildingBlocks.Infrastructure.Integration;

internal class EventRegistry : IEventRegistry
{
    private readonly Dictionary<string, Type> _types = new();

    public void Register(string key, Type type)
    {
        if (_types.ContainsKey(key))
        {
            return;
        }

        _types.Add(key, type);
    }

    public Type Navigate(string key)
    {
        return _types[key];
    }
}