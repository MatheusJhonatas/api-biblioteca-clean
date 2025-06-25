using Biblioteca.Domain.Shared.Entities;

namespace Biblioteca.Domain.Entities;

public class Livro : Entity
{
    #region Constructors
     public Livro(string titulo, string autor, string genero, int anoPublicacao, bool disponivel) : base(Guid.NewGuid())
     {
        Titulo = titulo;
        Autor = autor;
        Genero = genero;
        AnoPublicacao = anoPublicacao;
        Disponivel = disponivel;
     }
     
     #endregion
    public string Titulo { get;} = string.Empty;
    public string Autor { get;} = string.Empty;
    public string Genero { get;} = string.Empty;
    public int AnoPublicacao { get; } = 0;
    public bool Disponivel { get; } = true;

}