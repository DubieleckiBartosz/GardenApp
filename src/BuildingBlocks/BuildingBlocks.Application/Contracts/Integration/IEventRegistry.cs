namespace BuildingBlocks.Application.Contracts.Integration;

public interface IEventRegistry
{
    void Register(string key, Type type);

    Type Navigate(string key);
}