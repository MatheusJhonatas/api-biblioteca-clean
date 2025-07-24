using System;

namespace Biblioteca.Domain.Entities;

public abstract class Entity(Guid id) : IEquatable<Guid>, IEquatable<Entity>
{
    #region Propriedades
    public Guid Id { get; init; } = id;
    #endregion

    #region Métodos
    public bool Equals(Entity? other)
    {
        if (other is null) return false;
        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }
    public bool Equals(Guid other) => Id == other;
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Entity)obj);
    }
    public override int GetHashCode() => Id.GetHashCode();
    public static bool operator ==(Entity? left, Entity? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }
    public static bool operator !=(Entity? left, Entity? right) => !(left == right);
    #endregion
}