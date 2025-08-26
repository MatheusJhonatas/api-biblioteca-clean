using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Biblioteca.Infrastructure.Repositories;

public class LeitorRepository : ILeitorRepository
{
    private readonly BibliotecaDbContext _context;

    public LeitorRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task AtualizarAsync(Leitor leitor)
    {
        _context.Usuarios.Update(leitor);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(Guid id)
    {
        var leitor = await ObterPorIdAsync(id);
        if (leitor != null)
        {
            _context.Usuarios.Remove(leitor);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Leitor> ObterPorEmailAsync(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(l => l.Email.EnderecoEmail == email);
    }

    public async Task<Leitor> ObterPorIdAsync(Guid id)
    {
        return await _context.Usuarios
            .Include(l => l.Emprestimos)
            .Include(l => l.Reservas)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<IEnumerable<Leitor>> ObterTodosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task SalvarAsync(Leitor leitor)
    {
        if (leitor.Id == Guid.Empty)
        {
            await _context.Usuarios.AddAsync(leitor);
        }
        else
        {
            _context.Usuarios.Update(leitor);
        }
        await _context.SaveChangesAsync();
    }
}