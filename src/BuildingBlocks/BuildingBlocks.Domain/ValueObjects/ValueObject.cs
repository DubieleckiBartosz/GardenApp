namespace BuildingBlocks.Domain.ValueObjects;

public abstract class ValueObject
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });
    }

    public static bool operator ==(ValueObject? v1, ValueObject? v2)
    {
        if (ReferenceEquals(v1, null) && ReferenceEquals(v2, null))
        {
            return true;
        }

        if (ReferenceEquals(v1, null) || ReferenceEquals(v2, null))
        {
            return false;
        }

        return v1.Equals(v2);
    }

    public static bool operator !=(ValueObject? a, ValueObject? b)
    {
        return !(a == b);
    }
}