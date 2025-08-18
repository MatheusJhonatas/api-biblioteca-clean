using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<Livro?> ObterPorIdAsync(Guid id);
        Task<Livro> SalvarAsync(Livro livro);
        Task<Livro> AtualizarAsync(Livro livro);
        Task<Livro> RemoverAsync(Livro livro);
        Task<IEnumerable<Livro>> ListarDisponiveisAsync();

        Task<Livro?> ObterPorTituloEAutorAsync(string titulo, string nomeCompletoAutor);
        Task<Livro?> ObterPorISBNAsync(string isbn); // ✅ novo método
    }
}
