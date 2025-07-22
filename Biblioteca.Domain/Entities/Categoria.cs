namespace Biblioteca.Domain.Entities;
//Classe está como sealed para evitar herança, fazendo assim com que a Categoria seja uma entidade final.
// Isso é útil para garantir que a estrutura da categoria não seja alterada por heranças indesejadas.
public sealed class Categoria
{
    #region Propriedades

    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public List<Livro> Livros { get; private set; } = new List<Livro>();
    #endregion
    #region Construtores
    // Construtores para inicializar a categoria com nome e descrição, podendo receber um Id
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
    #endregion
    #region Métodos
    // Método para adicionar um livro à categoria
    // Regras: Um livro pode ter uma ou mais categorias.

    #endregion
}

