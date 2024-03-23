namespace BuildingBlocks.Domain.Entities;

public class Entity
{
    public int Id { get; }
    public int Version { get; protected set; }

    private readonly List<IDomainEvent> _events = new();
    private bool _versionIncremented;
    public List<IDomainEvent> Events => _events;

    protected void AddEvent(IDomainEvent @event)
    {
        if (!_events.Any() && !_versionIncremented)
        {
            Version++;
            _versionIncremented = true;
        }

        _events.Add(@event);
    }

    public void ClearEvents() => _events.Clear();

    protected void IncrementVersion()
    {
        if (_versionIncremented)
        {
            return;
        }

        Version++;
        _versionIncremented = true;
    }
}