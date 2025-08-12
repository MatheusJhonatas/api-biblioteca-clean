using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Infrastructure.Data.Mappings;

public class LivroMap : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder.ToTable("Livros");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Titulo)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(l => l.AnoPublicacao)
            .IsRequired();

        builder.Property(l => l.Disponivel)
            .IsRequired();

        // Configura Autor como Value Object Owned
        builder.OwnsOne(l => l.Autor);

        // Configura ISBN como Value Object Owned
        builder.OwnsOne(l => l.ISBN, isbn =>
        {
            isbn.Property(i => i.Codigo)
                .HasColumnName("ISBN")
                .IsRequired()
                .HasMaxLength(20);
            // ajuste conforme propriedades do ISBN VO
        });

        // Relacionamento muitos para muitos com Categoria
        builder
            .HasMany(l => l.Categorias)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "LivroCategoria",
                j => j.HasOne<Categoria>().WithMany().HasForeignKey("CategoriaId").HasConstraintName("FK_LivroCategoria_Categoria"),
                j => j.HasOne<Livro>().WithMany().HasForeignKey("LivroId").HasConstraintName("FK_LivroCategoria_Livro"),
                j =>
                {
                    j.HasKey("LivroId", "CategoriaId");
                    j.ToTable("LivroCategoria");
                });

        // Se CategoriaId for necessário, configure aqui, mas parece redundante com Categorias
        builder.Ignore(l => l.CategoriaId); // Ignorando porque há coleção Categorias que representa o relacionamento

    }
}

