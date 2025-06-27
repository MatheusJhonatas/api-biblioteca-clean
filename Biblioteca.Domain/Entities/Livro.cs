using Biblioteca.Domain.Shared.Entities;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Domain.Entities;

public class Livro : Entity
{
    #region Constructors
     public Livro(string titulo, string autor, Genero genero, int anoPublicacao, bool disponivel) : base(Guid.NewGuid())
     {
        Titulo = titulo;
        Autor = autor;
        Genero = genero;
        AnoPublicacao = anoPublicacao;
        Disponivel = disponivel;
     }
     
     #endregion
     #region Properties
    public string Titulo { get;} = string.Empty;
    public string Autor { get;} = string.Empty;
    public Genero Genero { get; } 
    public int AnoPublicacao { get; } = 0;
    public bool Disponivel { get; } = true;
    #endregion

}