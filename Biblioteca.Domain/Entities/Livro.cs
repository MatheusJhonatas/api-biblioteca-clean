using Biblioteca.Domain.Shared.Entities;

namespace Biblioteca.Domain.Entities;

public class Livro : Entity
{
    public Livro() : base(Guid.NewGuid())
    {

    }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Genero { get; set; }
    public int AnoPublicacao { get; set; }
    public bool Disponivel { get; set; }

}