namespace Biblioteca.Domain.Interfaces;

public interface IEmprestimoRepository
{
    Task<Emprestimo> ObterPorIdAsync(Guid id);
    Task SalvarAsync(Emprestimo emprestimo);
    Task AtualizarAsync(Emprestimo emprestimo);
    Task<IEnumerable<Emprestimo>> ObterAtivosPorLeitorAsync(Guid leitorId);
    Task<IEnumerable<Emprestimo>> ListarTodosAsync();
}