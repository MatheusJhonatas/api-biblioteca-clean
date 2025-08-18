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

        public Livro ObterPorId(Guid id)
        {
            return _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categorias)
                .FirstOrDefault(l => l.Id == id);
        }

        public void Salvar(Livro livro)
        {
            _context.Livros.Add(livro);
            _context.SaveChanges();
        }

        public void Atualizar(Livro livro)
        {
            _context.Livros.Update(livro);
            _context.SaveChanges();
        }

        public void Remover(Livro livro)
        {
            _context.Livros.Remove(livro);
            _context.SaveChanges();
        }

        public IEnumerable<Livro> ListarDisponiveis()
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
                .AsEnumerable() // força execução em memória
                .FirstOrDefault(l =>
                    l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase) &&
                    l.Autor.NomeCompleto.ToString().Equals(nomeCompletoAutor, StringComparison.OrdinalIgnoreCase)
                );
        }

        public async Task<Livro?> ObterPorISBNAsync(string isbn)
        {
            return await _context.Livros
                .FirstOrDefaultAsync(l => l.ISBN.Codigo == isbn);
        }
    }
}
