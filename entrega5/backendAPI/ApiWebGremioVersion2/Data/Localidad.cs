using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiWebGremioVersion2.Data
{
    public class Localidad
    {

        public int ID { get; set; }
        public int codigoPostal { get; set; }
        public string nombre { get; set; }

        // Propiedades de navegación

        public int idProvincia { get; set; }

        public Provincia Provincia { get; set; }

        public ICollection<Consultorio> consultorios { get; set; }

    }
}
