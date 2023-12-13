using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWebGremioVersion2.Data
{
    public class Agremiación
    {
        public int id { get; set; }

        public DateTime fechaInicio { get; set; }

        public DateTime fechaFin { get; set; }

        public string estadoOdontologo  {get;set;}

        public int nroMatricula { get; set; }

        // Propiedades de navegación

        public int idOdontologo { get; set; }

        public Odontologo Odontologo { get; set; }
        public ICollection<Cuota> Cuotas { get; set; }

    }
}
