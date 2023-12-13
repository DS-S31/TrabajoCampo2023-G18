using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiWebGremioVersion2.Data
{
    public class Provincia
    {

        public int ID { get; set; }
        public string nombre { get; set; }

        public ICollection<Localidad> Localidades { get; set; }
    }
}
