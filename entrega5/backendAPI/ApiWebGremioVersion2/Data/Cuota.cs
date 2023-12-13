using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiWebGremioVersion2.Data
{
    public class Cuota
    {

        public int ID { get; set; }

        public DateTime fechaVencimiento { get; set; }

        public double monto { get; set; }

        public DateTime periodo { get; set; }

        public string estadoCouta { get; set; }

        // Propiedades de navegación


        public int idAgremiacion { get; set; }

        public Agremiación Agremiación { get; set; }

    }
}
