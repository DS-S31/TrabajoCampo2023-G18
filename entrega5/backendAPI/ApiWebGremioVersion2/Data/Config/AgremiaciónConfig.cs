using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ApiWebGremioVersion2.Data.Config
{
    public class AgremiaciónConfig:IEntityTypeConfiguration<Agremiación>
    {
        public void Configure(EntityTypeBuilder<Agremiación> builder) {

            builder.ToTable("Agremiación");
            builder.HasKey(x => x.id);

            builder.Property(x => x.id).UseIdentityColumn();

            builder.HasOne(a => a.Odontologo)
               .WithMany(o => o.Agremiaciones)
               .HasForeignKey(a => a.idOdontologo);

            builder.HasIndex(o => o.nroMatricula).IsUnique();

            builder.Property(o => o.estadoOdontologo).IsRequired().HasMaxLength(100);
            builder.Property(o => o.fechaFin).IsRequired();
            builder.Property(o => o.fechaInicio).IsRequired();
            builder.Property(o => o.id).IsRequired();
            builder.Property(o => o.nroMatricula).IsRequired();
            builder.Property(o => o.idOdontologo).IsRequired();

        }
}
}
