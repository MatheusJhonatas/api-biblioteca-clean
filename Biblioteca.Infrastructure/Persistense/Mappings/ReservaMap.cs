using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Biblioteca.Infrastructure.Persistence.Mappings;

public class ReservaMap : IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> builder)
    {
        builder.ToTable("Reservas");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.DataReserva)
            .IsRequired();

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>(); // Salvar enum como texto, opcional

        builder.HasOne(r => r.Leitor)
            .WithMany(l => l.Reservas)
            .HasForeignKey("LeitorId")
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com Livro
        builder.HasOne(r => r.Livro)
            .WithMany() // supondo que Livro não tem coleção Reservas
            .HasForeignKey("LivroId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
