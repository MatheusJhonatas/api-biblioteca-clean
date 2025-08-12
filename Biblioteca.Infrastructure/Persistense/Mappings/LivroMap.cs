using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Biblioteca.Infrastructure.Persistence.Mappings;

public class LivroMap : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder.ToTable("Livros");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(l => l.ISBN)
            .IsRequired()
            .HasMaxLength(13);

        // Relacionamento com Autor
        builder.HasOne(l => l.Autor)
            .WithMany() // ou .WithMany(a => a.Livros) se existir na entidade Autor
            .HasForeignKey("AutorId")
            .OnDelete(DeleteBehavior.Restrict);

        builder
    .HasMany(l => l.Categorias)
    .WithMany() // ou .WithMany(c => c.Livros) se Categoria tiver coleção de livros
    .UsingEntity<Dictionary<string, object>>(
        "LivroCategoria",
        j => j.HasOne<Categoria>().WithMany().HasForeignKey("CategoriaId"),
        j => j.HasOne<Livro>().WithMany().HasForeignKey("LivroId"),
        j =>
        {
            j.HasKey("LivroId", "CategoriaId");
            j.ToTable("LivroCategoria");
        });
    }
}
