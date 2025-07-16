namespace Biblioteca.Domain.Entities;
//Classe está como sealed para evitar herança, fazendo assim com que a Categoria seja uma entidade final.
// Isso é útil para garantir que a estrutura da categoria não seja alterada por heranças indesejadas.
public sealed class Categoria
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public Categoria(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
    }
    public Categoria(int id, string nome, string descricao)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
    }

    public ICollection<Livro> Livros { get; private set; } = new List<Livro>();
    // Regras: Um livro pode ter uma ou mais categorias.
}

