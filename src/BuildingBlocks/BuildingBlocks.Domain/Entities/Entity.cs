using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Domain.Entities;

public class Entity
{
    //https://medium.com/@quocnguyen2501/int-or-guid-for-your-primary-key-what-your-choice-927f40c9dc08
    //https://www.reddit.com/r/dotnet/comments/ha4kyc/when_do_you_decide_whether_to_use_a_guid_or_an/
    public int Id { get; protected set; }

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

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (Id.Equals(default) || other.Id.Equals(default))
        {
            return false;
        }

        return Id.Equals(other.Id);
    }

    public static bool operator ==(Entity? a, Entity? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity? a, Entity? b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Id);
    }
}