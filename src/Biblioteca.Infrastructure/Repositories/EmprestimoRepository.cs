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

    public async Task AtualizarAsync(Emprestimo emprestimo)
    {
        _context.Emprestimos.Update(emprestimo);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Emprestimo>> ObterAtivosPorLeitorAsync(Guid leitorId)
    {
        return await _context.Emprestimos
            .Where(e => e.LeitorId == leitorId && e.DataRealDevolucao == null)
            .ToListAsync();
    }

    public async Task<Emprestimo> ObterPorIdAsync(Guid id)
    {
        return await _context.Emprestimos.FindAsync(id);
    }


    public async Task<IEnumerable<Emprestimo>> ListarTodosAsync()
    {
        return await _context.Emprestimos.Include(e => e.Leitor)
    .ThenInclude(l => l.NomeCompleto)
    .Include(e => e.Livro)
    .ToListAsync();
    }

    public async Task SalvarAsync(Emprestimo emprestimo)
    {
        _context.Emprestimos.Add(emprestimo);
        await _context.SaveChangesAsync();
    }
}
