
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Biblioteca.Infrastructure.Persistence.Mappings;

public class EmprestimoMap : IEntityTypeConfiguration<Emprestimo>
{
    public void Configure(EntityTypeBuilder<Emprestimo> builder)
    {
        // Nome da tabela
        builder.ToTable("Emprestimos");
        // Chave primária
        builder.HasKey(e => e.Id);
        // Data de empréstimo
        builder.Property(e => e.DataEmprestimo)
            .IsRequired();

        // Data prevista de devolução
        builder.Property(e => e.DataPrevistaDevolucao)
            .IsRequired();

        // Data real de devolução
        builder.Property(e => e.DataRealDevolucao)
            .IsRequired(false); // nullable
        // Status
        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<string>(); // opcional, para salvar enum como texto (ou use int padrão)

        // Relacionamento com Leitor (Usuario)
        builder.HasOne(e => e.Usuario)
            .WithMany(l => l.Emprestimos) // assume coleção Emprestimos em Leitor
            .HasForeignKey("UsuarioId")   // FK no banco como coluna "UsuarioId"
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com Livro
        builder.HasOne(e => e.Livro)
            .WithMany() // supondo que Livro não tem coleção Emprestimos
            .HasForeignKey("LivroId")
            .OnDelete(DeleteBehavior.Restrict);

        // Ignorar as propriedades que não existem fisicamente (se for o caso)
        // Aqui, UsuarioId e LivroId são getters e não propriedades físicas, 
        // então você pode mapear a FK via string no HasForeignKey
        builder.HasOne(e => e.Usuario)
            .WithMany(l => l.Emprestimos)
            .HasForeignKey("UsuarioId");
    }
}
