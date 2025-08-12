using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Persistence.Mappings;

public class LeitorMap : IEntityTypeConfiguration<Leitor>
{
    public void Configure(EntityTypeBuilder<Leitor> builder)
    {
        builder.ToTable("Leitores");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.DataCadastro)
            .IsRequired();

        builder.Property(l => l.LimiteEmprestimosAtivos)
            .IsRequired();

        // NomeCompleto (VO)
        builder.OwnsOne(l => l.NomeCompleto, nome =>
        {
            nome.Property(n => n.PrimeiroNome)
                .HasColumnName("PrimeiroNome")
                .IsRequired()
                .HasMaxLength(50);

            nome.Property(n => n.UltimoNome)
                .HasColumnName("Sobrenome")
                .IsRequired()
                .HasMaxLength(100);
        });

        // Email (VO)
        builder.OwnsOne(l => l.Email, email =>
        {
            email.Property(e => e.EnderecoEmail)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(150);
        });

        // CPF (VO)
        builder.OwnsOne(l => l.CPF, cpf =>
        {
            cpf.Property(c => c.Numero)
                .HasColumnName("CPF")
                .IsRequired()
                .HasMaxLength(11);
        });

        // Endereco (VO)
        builder.OwnsOne(l => l.Endereco, endereco =>
        {
            endereco.Property(e => e.Rua)
                .HasColumnName("Rua")
                .HasMaxLength(150);

            endereco.Property(e => e.Numero)
                .HasColumnName("Numero")
                .HasMaxLength(10);

            endereco.Property(e => e.Complemento)
                .HasColumnName("Complemento")
                .HasMaxLength(50);

            endereco.Property(e => e.Bairro)
                .HasColumnName("Bairro")
                .HasMaxLength(100);

            endereco.Property(e => e.Cidade)
                .HasColumnName("Cidade")
                .HasMaxLength(100);

            endereco.Property(e => e.Estado)
                .HasColumnName("Estado")
                .HasMaxLength(2);

            endereco.Property(e => e.CEP)
                .HasColumnName("CEP")
                .HasMaxLength(8);
        });

        // Relacionamento 1:N com Emprestimos (campo privado _emprestimos)
        builder.HasMany<Emprestimo>("_emprestimos")
            .WithOne()
            .HasForeignKey("UsuarioId")
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento 1:N com Reservas (campo privado _reservas)
        builder.HasMany<Reserva>("_reservas")
            .WithOne()
            .HasForeignKey("UsuarioId")
            .OnDelete(DeleteBehavior.Cascade);

        // Ignora propriedades calculadas
        builder.Ignore(l => l.EstaInadimplente);

        // IMPORTANTE: Não use PropertyAccessMode nem tente mapear a propriedade pública Emprestimos/Reservas!
    }
}