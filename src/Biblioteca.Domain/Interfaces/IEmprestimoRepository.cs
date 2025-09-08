namespace Biblioteca.Domain.Interfaces;

public interface IEmprestimoRepository
{
    Emprestimo ObterPorId(Guid id);
    void Salvar(Emprestimo emprestimo);
    void Atualizar(Emprestimo emprestimo);
    IEnumerable<Emprestimo> ObterAtivosPorLeitor(Guid leitorId);
}