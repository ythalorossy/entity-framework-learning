namespace Domain.Common;

public abstract class Entity<TId>(TId id)
{
    public TId Id { get; } = id;

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> other && EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
        return Id?.GetHashCode() ?? 0;
    }
}