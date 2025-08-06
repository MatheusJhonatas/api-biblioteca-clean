using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

public interface ILivroRepository
{
    Livro ObterPorId(Guid id);
    void Atualizar(Livro livro);
    void Salvar(Livro livro);
    IEnumerable<Livro> ListarDisponiveis();
}