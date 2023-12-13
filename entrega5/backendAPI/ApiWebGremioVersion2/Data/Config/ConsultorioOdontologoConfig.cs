using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ApiWebGremioVersion2.Data.Config
{
    public class ConsultorioOdontologoConfig: IEntityTypeConfiguration<ConsultorioOdontologo>
    {
        public void Configure(EntityTypeBuilder<ConsultorioOdontologo> builder)
        {

            builder.ToTable("ConsultorioOdontologo");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).UseIdentityColumn();


            builder
                .HasOne(co => co.Odontologo)
                .WithMany(o => o.Consultorios)
                .HasForeignKey(co => co.idOdontologo);

            builder
                .HasOne(co => co.Consultorio)
                .WithMany(o => o.Odontologos)
                .HasForeignKey(co => co.idConsultorio);


            builder.Property(o => o.idConsultorio).IsRequired();
            builder.Property(o => o.ID).IsRequired();
            builder.Property(o => o.idOdontologo).IsRequired();
        }
    }
}
