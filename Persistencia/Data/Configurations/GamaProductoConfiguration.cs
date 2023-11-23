using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Persistence.Data.Configurations
{
    public class GamaProductoConfiguration : IEntityTypeConfiguration<GamaProducto>
    {
        public void Configure(EntityTypeBuilder<GamaProducto> builder)
        {
            // Configuración de la entidad
            builder.HasKey(e => e.Gama).HasName("PRIMARY");

            builder.ToTable("gama_producto");

            builder.Property(e => e.Gama)
                .HasMaxLength(50)
                .HasColumnName("gama");
            builder.Property(e => e.DescripcionHtml)
                .HasColumnType("text")
                .HasColumnName("descripcion_html");
            builder.Property(e => e.DescripcionTexto)
                .HasColumnType("text")
                .HasColumnName("descripcion_texto");
            builder.Property(e => e.Imagen)
                .HasMaxLength(256)
                .HasColumnName("imagen");
        }
    }
}