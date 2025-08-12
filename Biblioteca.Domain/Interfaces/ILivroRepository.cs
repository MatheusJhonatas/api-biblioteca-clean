using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

public interface ILivroRepository
{
    Livro ObterPorId(Guid id);
    void Atualizar(Livro livro);
    void Salvar(Livro livro);
    void Remover(Livro livro);
    IEnumerable<Livro> ListarDisponiveis();
}