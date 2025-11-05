using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

public interface IEmprestimoService
{
    Emprestimo RealizarEmprestimo(Leitor leitor, Livro livro);
    void DevolverLivro(Leitor leitor, Guid emprestimoId);
}