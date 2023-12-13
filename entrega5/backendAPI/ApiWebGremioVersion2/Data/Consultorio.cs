using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiWebGremioVersion2.Data
{
    public class Consultorio
    {

        public int ID { get; set; }
        public int numero { get; set; }
        public string calle { get; set; }

        // Propiedades de navegación

        public int idLocalidad { get; set; }
        public Localidad Localidad { get; set; }

        public ICollection<ConsultorioOdontologo> Odontologos { get; set; }


    }
}
