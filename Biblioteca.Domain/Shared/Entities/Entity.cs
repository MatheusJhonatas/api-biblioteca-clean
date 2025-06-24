namespace Biblioteca.Domain.Shared.Entities;
//usando o IEquatable para comparar entidades, pois o Entity Framework precisa comparar as entidades para verificar se são iguais 
public abstract class Entity(Guid id) : IEquatable<Guid>
{
    #region Properties
    public Guid Id { get; set; } = id;
    #endregion

    #region Equatable Implementation
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != GetType())
            return false;

        return Equals((Entity)obj);
    }
    public bool Equals(Entity? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Id.Equals(other.Id);
    }
    public bool Equals(Guid id) => Id == id;
    public override int GetHashCode() => Id.GetHashCode();
    #endregion


}