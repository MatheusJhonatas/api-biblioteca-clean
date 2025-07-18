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
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Entity)obj);
    }
    public bool Equals(Guid other) => Id == other;
    public override bool Equals(object? obj)
    {
        if (obj is Entity otherEntity)
        {
            return Equals(otherEntity);
        }
        return false;
    }
    #endregion
}