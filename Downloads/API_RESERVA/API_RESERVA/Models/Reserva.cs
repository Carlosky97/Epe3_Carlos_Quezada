namespace API_RESERVA.Models
{
    public class Reserva
    {
        public int id_res{ get; set; }
        public string? especialidad { get; set; }
        public DateTime dia_res { get; set; }
        public int paciente_id_pac { get; set; }
    }
}
