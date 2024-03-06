namespace BuildingBlocks.Application.Contracts.Integration;

public abstract class IntegrationEventNavigator
{
    protected Dictionary<string, Type> _integrationNavigator;

    protected IntegrationEventNavigator()
    {
        _integrationNavigator = new();
    }

    public void AddNavigation<T>(string key) where T : IntegrationEvent
    {
        if (_integrationNavigator.ContainsKey(key))
        {
            return;
        }

        _integrationNavigator.Add(key, typeof(T));
    }

    public Type? GetNavigation<T>(string key) where T : IntegrationEvent
        => _integrationNavigator.TryGetValue(key, out var navigation) ? navigation : null;
}