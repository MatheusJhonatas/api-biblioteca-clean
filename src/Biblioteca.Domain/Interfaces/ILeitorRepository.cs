using Biblioteca.Domain.Entities;
namespace Biblioteca.Domain.Interfaces;

public interface ILeitorRepository
{
    Task<Leitor> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Leitor>> ObterTodosAsync();
    Task<Leitor> ObterPorEmailAsync(string email);
    Task AtualizarAsync(Leitor leitor);
    Task SalvarAsync(Leitor leitor);
    Task DeletarAsync(Guid id);
}