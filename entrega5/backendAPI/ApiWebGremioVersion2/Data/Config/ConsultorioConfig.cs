using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiWebGremioVersion2.Data.Config
{
    public class ConsultorioConfig : IEntityTypeConfiguration<Consultorio>
    {
        public void Configure(EntityTypeBuilder<Consultorio> builder)
        {
                builder.ToTable("Consultorio");
                builder.HasKey(x => x.ID);

                builder.Property(x => x.ID).UseIdentityColumn();

                builder.HasOne(c => c.Localidad)
               .WithMany(l => l.consultorios)
               .HasForeignKey(c => c.idLocalidad);


            builder.Property(o => o.calle).IsRequired().HasMaxLength(100);
            builder.Property(o => o.ID).IsRequired();
            builder.Property(o => o.numero).IsRequired();
            builder.Property(o => o.idLocalidad).IsRequired();

        }
        }
    }

