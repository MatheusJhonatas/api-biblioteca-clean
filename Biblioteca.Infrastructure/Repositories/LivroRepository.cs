using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly BibliotecaDbContext _context;

        public LivroRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<Livro?> ObterPorIdAsync(Guid id)
        {
            return await _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categorias)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Livro> SalvarAsync(Livro livro)
        {
            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<Livro> AtualizarAsync(Livro livro)
        {
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<Livro> RemoverAsync(Livro livro)
        {
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<IEnumerable<Livro>> ListarDisponiveisAsync()
        {
            return _context.Livros
                .Include(l => l.Autor)
                .Where(l => l.Disponivel)
                .ToList();
        }

        public async Task<Livro?> ObterPorTituloEAutorAsync(string titulo, string nomeCompletoAutor)
        {
            return await _context.Livros
    .Include(l => l.Autor)
    .FirstOrDefaultAsync(l =>
        l.Titulo.ToLower() == titulo.ToLower() &&
        (l.Autor.NomeCompleto.PrimeiroNome + " " + l.Autor.NomeCompleto.UltimoNome).ToLower() == nomeCompletoAutor.ToLower()
    );
        }

        public async Task<Livro?> ObterPorISBNAsync(string isbn)
        {
            return await _context.Livros
                .FirstOrDefaultAsync(l => l.ISBN.Codigo == isbn)
                ;
        }
    }
}
