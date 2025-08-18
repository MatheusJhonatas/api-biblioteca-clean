using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly BibliotecaDbContext _context;

        public AutorRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<Autor?> ObterPorEmailAsync(string email)
        {
            return await _context.Autores
                .FirstOrDefaultAsync(a => a.Email.EnderecoEmail.ToLower() == email.ToLower());
        }

        public async Task<Autor?> ObterPorIdAsync(Guid id)
        {
            return await _context.Autores.FindAsync(id);
        }

        public void Salvar(Autor autor)
        {
            _context.Autores.Add(autor);
            _context.SaveChanges();
        }
    }
}
