using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiWebGremioVersion2.Data.Config
{
    public class ProvinciaConfig : IEntityTypeConfiguration<Provincia>
    {
        public void Configure(EntityTypeBuilder<Provincia> builder)
        {

            builder.ToTable("Provincia");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).UseIdentityColumn();

            //builder.HasData(new List<Provincia>
            //{ new Provincia()
            //{
            //    ID = 1,
            //    nombre = "buenos aires"
            //},

            //new Provincia()
            //{
            //     ID = 2,
            //     nombre = "córdoba"
            //}

            //});
    }
    }
}