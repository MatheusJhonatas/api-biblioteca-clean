using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Livro ObterPorId(Guid id);
        void Salvar(Livro livro);
        void Atualizar(Livro livro);
        void Remover(Livro livro);
        IEnumerable<Livro> ListarDisponiveis();

        Task<Livro?> ObterPorTituloEAutorAsync(string titulo, string nomeCompletoAutor);
        Task<Livro?> ObterPorISBNAsync(string isbn); // ✅ novo método
    }
}
