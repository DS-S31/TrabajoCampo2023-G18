using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiWebGremioVersion2.Data.Config
{
    public class LocalidadConfig: IEntityTypeConfiguration<Localidad>
    {
        public void Configure(EntityTypeBuilder<Localidad> builder)
        {
            builder.ToTable("Localidad");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).UseIdentityColumn();

            builder
           .HasOne(l => l.Provincia)
           .WithMany(p => p.Localidades)
           .HasForeignKey(l => l.idProvincia);


        }
    }
}
