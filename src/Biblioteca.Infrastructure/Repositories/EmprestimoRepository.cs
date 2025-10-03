using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

    public IEnumerable<Emprestimo> ObterAtivosPorLeitor(Guid leitorId)
    {
        return _context.Emprestimos
            .Where(e => e.LeitorId == leitorId && e.DataRealDevolucao == null)
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
    public async Task<IEnumerable<Emprestimo>> ListarTodosAsync()
    {
        return await _context.Emprestimos.ToListAsync();
    }
}