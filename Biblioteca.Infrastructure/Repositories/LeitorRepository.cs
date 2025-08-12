using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Persistense;

public class LeitorRepository : ILeitorRepository
{
    private readonly BibliotecaDbContext _context;

    public LeitorRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public void Atualizar(Leitor leitor)
    {
        _context.Usuarios.Update(leitor);
        _context.SaveChanges();
    }

    public Leitor ObterPorId(Guid id)
    {
        return _context.Usuarios.Find(id);
    }

    public void Salvar(Leitor leitor)
    {
        _context.Usuarios.Add(leitor);
        _context.SaveChanges();
    }
}