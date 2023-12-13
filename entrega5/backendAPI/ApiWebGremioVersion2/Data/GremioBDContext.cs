using ApiWebGremioVersion2.Data.Config;
using ApiWebGremioVersion2.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWebGremioVersion2.Data
{
    public class GremioBDContext : DbContext
    {

        public GremioBDContext(DbContextOptions<GremioBDContext> options) : base(options)
        {

        }
        public DbSet<Odontologo> Odontologo { get; set; }
        public DbSet<Consultorio> Consultorio { get; set; }
        public DbSet<ConsultorioOdontologo> ConsultorioOdontologo { get; set; }
        public DbSet<Localidad> Localidad { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Agremiación> Agremiación { get; set; }
        public DbSet<Cuota> Cuota { get; set; }
        public DbSet<MontoAnual> MontoAnual { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfiguration(new OdontologoConfig());

            modelBuilder.ApplyConfiguration(new ConsultorioConfig());

            modelBuilder.ApplyConfiguration(new ConsultorioOdontologoConfig());

            modelBuilder.ApplyConfiguration(new AgremiaciónConfig());

            modelBuilder.ApplyConfiguration(new LocalidadConfig());

            modelBuilder.ApplyConfiguration(new CuotaConfig());



        }
    }
}
