using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Biblioteca.Infrastructure.Persistence.Mappings;

public class AutorMap : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.ToTable("Autores");

        builder.HasKey(a => a.Id);

        // Value Object NomeCompleto
        builder.OwnsOne(a => a.NomeCompleto, nome =>
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

        // Email (Value Object)
        builder.OwnsOne(a => a.Email, email =>
        {
            email.Property(e => e.EnderecoEmail)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(150);
        });

        builder.Property(a => a.DataNascimento)
            .IsRequired();
    }
}
