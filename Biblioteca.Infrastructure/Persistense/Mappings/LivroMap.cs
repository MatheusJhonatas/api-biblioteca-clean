using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;
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

        // Mapeando ISBN como Value Object (usando ValueConverter)
        builder.Property(l => l.ISBN)
            .HasConversion(
                v => v.Codigo,           // Como salvar no banco (string)
                v => new ISBN(v)         // Como ler do banco (Value Object)
            )
            .HasColumnName("ISBN")
            .IsRequired()
            .HasMaxLength(20);

        // Relacionamento com Autor
        builder.HasOne(l => l.Autor)
            .WithMany() // ou .WithMany(a => a.Livros) se existir na entidade Autor
            .HasForeignKey("AutorId")
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento N:N com Categorias (usando campo privado se desejar)
        builder
    .HasMany<Categoria>("_categorias")
    .WithMany()
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