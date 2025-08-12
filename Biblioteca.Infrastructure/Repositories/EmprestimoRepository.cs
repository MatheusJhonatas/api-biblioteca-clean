using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Persistense;

namespace Biblioteca.Infrastructure.Repositories;

public class EmprestimoRepository : IEmprestimoRepository
{
    private readonly BibliotecaDbContext _context;

    public EmprestimoRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public void Atualizar(Emprestimo emprestimo)
    {
        _context.Emprestimos.Update(emprestimo);
        _context.SaveChanges();
    }

    public IEnumerable<Emprestimo> ObterAtivosPorLeitor(Guid usuarioId)
    {
        return _context.Emprestimos
            .Where(e => e.UsuarioId == usuarioId && e.DataRealDevolucao == null)
            .ToList();
    }

    public Emprestimo ObterPorId(Guid id)
    {
        return _context.Emprestimos.Find(id);
    }

    public void Salvar(Emprestimo emprestimo)
    {
        _context.Emprestimos.Add(emprestimo);
        _context.SaveChanges();
    }
}