using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using API_RESERVA.Controllers;
namespace API_RESERVA.Models
{
    public class Medico
    {

        public int id_med { get; set; } 
        public string? nombre_med { get; set; }
        public string? apellido_med { get; set; }
        public string? run_med { get; set; }
        public string? eunacom { get; set; }
        public string? nacionalidad_med { get; set; }
        public string? especialidad { get; set; }
        public string? horarios { get; set; }
        public int tarifahr { get; set; }
    }
}
