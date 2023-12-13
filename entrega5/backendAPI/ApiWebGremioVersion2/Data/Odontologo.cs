using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiWebGremioVersion2.Data
{
    public class Odontologo
    {

        public int ID { get; set; }
        public int dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }

        // Propiedades de navegación

        public ICollection<ConsultorioOdontologo> Consultorios { get; set; }
        public ICollection<Agremiación> Agremiaciones { get; set; }


    }
}
