using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore;

public class ReservaRepository : IReservaRepository
{
    private readonly BibliotecaDbContext _context;

    public ReservaRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public Reserva ObterPorId(Guid id)
    {
        return _context.Reservas.Include(r => r.Livro).Include(r => r.Usuario).FirstOrDefault(r => r.Id == id);
    }

    public void Salvar(Reserva reserva)
    {
        _context.Reservas.Add(reserva);
        _context.SaveChanges();
    }

    public void Atualizar(Reserva reserva)
    {
        _context.Reservas.Update(reserva);
        _context.SaveChanges();
    }

    public IEnumerable<Reserva> ObterPorLivro(Guid livroId)
    {
        return _context.Reservas.Include(r => r.Livro).Include(r => r.Usuario).Where(r => r.LivroId == livroId).ToList();
    }
}
