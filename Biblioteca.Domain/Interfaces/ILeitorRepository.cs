using Biblioteca.Domain.Entities;
namespace Biblioteca.Domain.Interfaces;

public interface ILeitorRepository
{
    Leitor ObterPorId(Guid id);
    void Atualizar(Leitor leitor);
    void Salvar(Leitor leitor);
}