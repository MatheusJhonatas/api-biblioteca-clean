// Categoria/Gênero (Category ou Genre)
// Atributos: Nome, Descrição.

// Regras: Um livro pode ter uma ou mais categorias.
namespace Biblioteca.Domain.Entities;

public sealed class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public ICollection<Livro> Livros { get; set; } = new List<Livro>();
}

