using Microsoft.EntityFrameworkCore;
using Biblioteca.Domain.Entities;

namespace Biblioteca.Infrastructure.Persistense
{
    public class BibliotecaDbContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Leitor> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais de entidades podem ser feitas aqui
            // modelBuilder.Entity<Livro>().ToTable("Livros");
        }
    }
}