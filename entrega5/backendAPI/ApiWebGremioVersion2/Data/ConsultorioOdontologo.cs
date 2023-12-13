namespace ApiWebGremioVersion2.Data
{
    public class ConsultorioOdontologo
    {
        public int ID { get; set; }
        public int idOdontologo { get; set; }
        public int idConsultorio { get; set; }

        // Propiedades de navegación
        public Odontologo Odontologo { get; set; }
        public Consultorio Consultorio { get; set; }
    }
}
