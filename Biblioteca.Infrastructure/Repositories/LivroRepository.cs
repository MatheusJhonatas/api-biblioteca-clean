using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        // Contexto do banco de dados, injetado via construtor
        private readonly BibliotecaDbContext _context;
        //Construtor que recebe o contexto do banco de dados
        public LivroRepository(BibliotecaDbContext context)
        {
            _context = context;
        }
        //método para obter um livro pelo ID, incluindo autor e categorias 
        public Livro ObterPorId(Guid id)
        {
            return _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categorias)
                .FirstOrDefault(l => l.Id == id);
        }
        //metodo para salvar um livro, incluindo autor e categorias
        public void Salvar(Livro livro)
        {
            _context.Livros.Add(livro);
            _context.SaveChanges();
        }
        //método para atualizar um livro, incluindo autor e categorias
        public void Atualizar(Livro livro)
        {
            _context.Livros.Update(livro);
            _context.SaveChanges();
        }
        /// Método para remover um livro do banco de dados.
        public void Remover(Livro livro)
        {
            _context.Livros.Remove(livro);
            _context.SaveChanges();
        }
        //método para listar todos os livros disponíveis, utilizando IEnumerable para retorno 
        public IEnumerable<Livro> ListarDisponiveis()
        {
            return _context.Livros
                .Include(l => l.Autor)
                .Where(l => l.Disponivel)
                .ToList();
        }
    }
}
