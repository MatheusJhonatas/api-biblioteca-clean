namespace Biblioteca.Domain.Entities;

public abstract class Entity(Guid id) :IEquatable<Guid>, IEquatable<Entity>
{
    #region Propriedades
    public int Id { get; protected set; }
    #endregion

    #region Construtores
    protected Entity(int id)
    {
        Id = id;
    }
    #endregion

    // Regras: Todas as entidades devem ter um Id único.
}