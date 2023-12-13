using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ApiWebGremioVersion2.Data.Config
{
    public class OdontologoConfig: IEntityTypeConfiguration<Odontologo>
    {
        public void Configure(EntityTypeBuilder<Odontologo> builder)
        {
            builder.ToTable("Odontologo");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).UseIdentityColumn();

            builder.Property(o => o.nombre).IsRequired().HasMaxLength(100);
            builder.Property(o => o.apellido).IsRequired().HasMaxLength(100);
            builder.Property(o => o.ID).IsRequired();
            builder.Property(o => o.dni).IsRequired();

            builder.HasIndex(o => o.dni).IsUnique();
        }
    }
}
