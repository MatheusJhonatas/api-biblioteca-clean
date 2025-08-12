using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Biblioteca.Infrastructure.Persistence.Mappings;

public class BibliotecarioMap : IEntityTypeConfiguration<Bibliotecario>
{
    public void Configure(EntityTypeBuilder<Bibliotecario> builder)
    {
        builder.ToTable("Bibliotecarios");

        builder.HasKey(b => b.Id);

        // NomeCompleto (Value Object)
        builder.OwnsOne(b => b.NomeCompleto, nome =>
        {
            nome.Property(n => n.PrimeiroNome)
                .HasColumnName("PrimeiroNome")
                .IsRequired()
                .HasMaxLength(50);

            nome.Property(n => n.UltimoNome) // ou Sobrenome, conforme seu VO
                .HasColumnName("Sobrenome")
                .IsRequired()
                .HasMaxLength(100);
        });

        // Email (Value Object)
        builder.OwnsOne(b => b.Email, email =>
        {
            email.Property(e => e.EnderecoEmail)
                 .HasColumnName("Email")
                 .IsRequired()
                 .HasMaxLength(150);
        });

        builder.Property(b => b.Matricula)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(b => b.Cargo)
            .IsRequired()
            .HasMaxLength(100);
    }
}
