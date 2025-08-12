using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Data.Mappings
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            // Nome da tabela (opcional)
            builder.ToTable("Categorias");

            // Chave primária (presumindo que Entity já tenha Id)
            builder.HasKey(c => c.Id);

            // Propriedade Nome: required, varchar(100)
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Propriedade Tipo: mapeada como int no banco
            builder.Property(c => c.Tipo)
                .IsRequired()
                .HasConversion<int>();

            // Se quiser configurar outras propriedades ou relacionamentos, configure aqui
        }
    }
}
