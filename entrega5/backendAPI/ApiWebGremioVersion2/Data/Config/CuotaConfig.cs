using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiWebGremioVersion2.Data.Config
{
    public class CuotaConfig : IEntityTypeConfiguration<Cuota>
    {
        public void Configure(EntityTypeBuilder<Cuota> builder)
        {

            builder.ToTable("Cuota");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).UseIdentityColumn();

            builder
           .HasOne(c => c.Agremiación)
           .WithMany(a => a.Cuotas)
           .HasForeignKey(c => c.idAgremiacion);

        }
    }
}