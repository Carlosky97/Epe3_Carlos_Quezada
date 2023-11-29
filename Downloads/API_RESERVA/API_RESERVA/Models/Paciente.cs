namespace API_RESERVA.Models
{
    public class Paciente
    {
        public int id_pac { get; set; }
        public string? nombre_pac { get; set; }
        public string? apellido_pac { get; set; }
        public string? run_pac { get; set; }
        public string? nacionalidad_pac { get; set; }
        public string? visa { get; set; }
        public string? genero { get; set; }
        public string? sintomas_pac { get; set; }
        public int medico_id_med { get; set; }
    }
}
