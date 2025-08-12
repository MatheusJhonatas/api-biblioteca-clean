using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Persistence;
namespace Biblioteca.Infrastructure.Repositories;

public class BibliotecarioRepository : IBibliotecarioRepository
{
    private readonly BibliotecaDbContext _context;
    public BibliotecarioRepository(BibliotecaDbContext context)
    {
        _context = context;
    }
    public Bibliotecario ObterPorId(Guid id)
    {
        return _context.Bibliotecarios.Find(id);
    }

    public void Salvar(Bibliotecario bibliotecario)
    {
        _context.Bibliotecarios.Add(bibliotecario);
        _context.SaveChanges();
    }
}