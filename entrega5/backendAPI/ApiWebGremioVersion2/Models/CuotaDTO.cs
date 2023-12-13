namespace ApiWebGremioVersion2.Models
{
    public class CuotaDTO
    {

        public int ID { get; set; }

        public DateOnly fechaVencimiento{ get; set; }

        public double monto { get; set; }

        public DateOnly periodo { get; set; }

        public string estadoCouta { get; set; }


    }
}
