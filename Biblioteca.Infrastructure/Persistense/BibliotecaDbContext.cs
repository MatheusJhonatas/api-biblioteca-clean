using Microsoft.EntityFrameworkCore;
using Biblioteca.Domain.Entities;
using Biblioteca.Infrastructure.Persistence.Mappings;

namespace Biblioteca.Infrastructure.Persistense
{
    public class BibliotecaDbContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Leitor> Usuarios { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Bibliotecario> Bibliotecarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais de entidades podem ser feitas aqui
            // modelBuilder.Entity<Livro>().ToTable("Livros");
            modelBuilder.ApplyConfiguration(new LivroMap());
            modelBuilder.ApplyConfiguration(new AutorMap());
            modelBuilder.ApplyConfiguration(new LeitorMap());
            modelBuilder.ApplyConfiguration(new EmprestimoMap());
            modelBuilder.ApplyConfiguration(new ReservaMap());
            modelBuilder.ApplyConfiguration(new BibliotecarioMap());
        }
    }
}