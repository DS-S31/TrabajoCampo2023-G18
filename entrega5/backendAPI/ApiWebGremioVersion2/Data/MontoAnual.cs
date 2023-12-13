using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiWebGremioVersion2.Data
{
    public class MontoAnual
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int año { get; set; }

        public double monto { get; set; }
    }
}
