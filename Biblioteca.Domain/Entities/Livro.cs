// Atributos: Título, Autor, ISBN (VO), Ano de Publicação, Categoria(s), Disponibilidade
// Regras: Pode estar disponível ou emprestado, ISBN deve ser válido
// Métodos: Emprestar, Devolver, Verificar disponibilidade
using Biblioteca.Domain.ValueObjects;
namespace Biblioteca.Domain.Entities
{
    public sealed class Livro
    {
        #region Propriedades
        public string Titulo { get; private set; }
        public Autor Autor { get; private set; }
        public ISBN ISBN { get; private set; }
        public int AnoPublicacao { get; private set; }
        public int CategoriaId { get; private set; }
        #endregion
        public List<Categoria> Categorias { get; private set; }
    }
}
