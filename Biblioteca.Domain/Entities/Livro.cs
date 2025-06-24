using Biblioteca.Domain.Shared.Entities;

namespace Biblioteca.Domain.Entities;

public class Livro : Entity
{
    #region Constructors
     public Livro() : base(Guid.NewGuid())
     {
        
     }
     #endregion
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public int AnoPublicacao { get; set; } = 0;
    public bool Disponivel { get; set; } = true;

}