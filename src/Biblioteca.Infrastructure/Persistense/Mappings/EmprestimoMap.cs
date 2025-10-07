
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Biblioteca.Infrastructure.Persistence.Mappings;

public class EmprestimoMap : IEntityTypeConfiguration<Emprestimo>
{
    public void Configure(EntityTypeBuilder<Emprestimo> builder)
    {
        builder.ToTable("Emprestimos");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.DataEmprestimo).IsRequired();
        builder.Property(e => e.DataPrevistaDevolucao).IsRequired();
        builder.Property(e => e.DataRealDevolucao).IsRequired(false);
        builder.Property(e => e.Status).IsRequired().HasConversion<string>();

        // Relacionamento com Leitor (Leitor)
        builder.HasOne(e => e.Leitor)
            .WithMany(l => l.Emprestimos)
            .HasForeignKey("LeitorId")
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com Livro
        builder.HasOne(e => e.Livro)
            .WithMany()
            .HasForeignKey("LivroId")
            .OnDelete(DeleteBehavior.Restrict);

    }
}