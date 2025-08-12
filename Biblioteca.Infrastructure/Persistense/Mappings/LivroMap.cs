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

        builder.Property(l => l.NumeroPaginas)
            .IsRequired();

        builder.Property(l => l.ISBN)
            .HasConversion(
                v => v.Codigo,
                v => new ISBN(v)
            )
            .HasColumnName("ISBN")
            .IsRequired()
            .HasMaxLength(20);

        builder.HasOne(l => l.Autor)
            .WithMany()
            .HasForeignKey("AutorId")
            .OnDelete(DeleteBehavior.Restrict);

        // Ignora a propriedade pública para evitar conflito com o campo privado
        builder.Ignore(l => l.Categorias);

        // Mapeia relacionamento N:N usando campo privado
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