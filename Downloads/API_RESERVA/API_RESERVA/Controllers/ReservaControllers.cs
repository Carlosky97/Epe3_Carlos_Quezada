using API_RESERVA.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace API_RESERVA.Controllers
{
    public class ReservaControllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ReservaController : ControllerBase
        {
            private readonly string StringConector;

            public ReservaController(IConfiguration config)
            {
                StringConector = config.GetConnectionString("MySqlConnection");
            }

            [HttpGet]
            public IActionResult ListarReservas()
            {
                try
                {
                    using (MySqlConnection conecta = new MySqlConnection(StringConector))
                    {
                        conecta.Open();

                        using (MySqlCommand command = new MySqlCommand("SELECT * FROM Reserva", conecta))
                        {
                            MySqlDataReader reader = command.ExecuteReader();
                            var reservas = new List<Reserva>();
                            while (reader.Read())
                            {
                                reservas.Add(new Reserva
                                {
                                    id_res = reader.GetInt32(0),
                                    especialidad = reader.GetString(1),
                                    dia_res = reader.GetDateTime(2),
                                    paciente_id_pac = reader.GetInt32(3)
                                });
                            }

                            return StatusCode(200, reservas);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "No se ha podido listar las reservas" + ex);
                }
            }

            [HttpGet("{id}")]
            public IActionResult ListarTodos(int id)
            {
                try
                {
                    using (MySqlConnection conecta = new MySqlConnection(StringConector))
                    {
                        conecta.Open();

                        var sentencia = "SELECT * FROM Reserva WHERE IdReserva = idReserva";

                        using (MySqlCommand command = new MySqlCommand(sentencia, conecta))
                        {
                            command.Parameters.Add(new MySqlParameter("idReserva", id));
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                var reservas = new Reserva()
                                {
                                    id_res = reader.GetInt32(0),
                                    especialidad = reader.GetString(1),
                                    dia_res = reader.GetDateTime(2),
                                    paciente_id_pac = reader.GetInt32(3)
                                };
                                return StatusCode(200, reservas);
                            }
                            else
                            {
                                return StatusCode(404, "Registro no encontrado");

                            }


                        }
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "No se ha podido obtener la informacion" + ex);
                }
            }

            [HttpPost]
            public IActionResult GuardarReservas([FromBody] Reserva reservas)
            {
                try
                {

                    using (MySqlConnection conector = new MySqlConnection(StringConector))
                    {

                        conector.Open();

                        string sentencia = "INSERT INTO Reserva (idReserva,Especialidad, DiaReserva, Paciente_idPaciente) VALUES (@idReserva, @Especialidad, @DiaReserva, @Paciente_idPaciente)";

                        MySqlCommand comando = new MySqlCommand(sentencia, conector);


                        comando.Parameters.Add(new MySqlParameter("idReserva", reservas.id_res));
                        comando.Parameters.Add(new MySqlParameter("Especialidad", reservas.especialidad));
                        comando.Parameters.Add(new MySqlParameter("DiaReserva", reservas.dia_res));
                        comando.Parameters.Add(new MySqlParameter("Paciente_idPaciente", reservas.paciente_id_pac));
 
                        comando.ExecuteNonQuery();

                        return StatusCode(201, "Reserva creado exitosamente");


                    }

                }
                catch (Exception ex)

                {
                    return StatusCode(500, "No se pudo guardar el registro por: " + ex);

                }
            }

            [HttpPost("{id}")]
            public IActionResult EditarReservas(int id, [FromBody] Reserva reservas)
            {
                try
                {


                    using (MySqlConnection conector = new MySqlConnection(StringConector))
                    {

                        conector.Open();

                        string sentencia = "UPDATE Reserva SET Especialidad = @Especialidad , DiaReserva= @DiaReserva, Paciente_idPaciente = @Paciente_idPaciente WHERE idReserva = @idReserva";

                        using (MySqlCommand comando = new MySqlCommand(sentencia, conector))
                        {

                            comando.Parameters.Add(new MySqlParameter("Especialidad", reservas.especialidad));
                            comando.Parameters.Add(new MySqlParameter("DiaReserva", reservas.dia_res));
                            comando.Parameters.Add(new MySqlParameter("Paciente_idPaciente", reservas.paciente_id_pac));
                            comando.Parameters.Add(new MySqlParameter("idReserva", id));

                            comando.ExecuteNonQuery();

                            return StatusCode(200, "Registro editado exitosamente");


                        }

                    }


                }
                catch (Exception ex)
                {

                    return StatusCode(500, "No se pudo editar el registro por: " + ex);

                }


            }
            [HttpDelete("id")]
            public ActionResult EliminarReservas(int id)
            {
                try
                {

                    using (MySqlConnection conector = new MySqlConnection(StringConector))
                    {

                        conector.Open();

                        string sentencia = "DELETE FROM Reserva WHERE idReserva = @idReserva";

                        using (MySqlCommand comando = new MySqlCommand(sentencia, conector))
                        {

                            comando.Parameters.Add(new MySqlParameter("idReserva", id));

                            int filas = comando.ExecuteNonQuery();

                            if (filas == 0)
                            {

                                return StatusCode(404, "Registro no encontrado");

                            }
                            else
                            {

                                return StatusCode(200, $"Persona con el IdReserva {id} ha sido eliminada exitosamente");

                            }


                        }

                    }

                }
                catch (Exception ex)
                {

                    return StatusCode(500, "No se pudo eliminar el registro " + ex);

                }

            }
        }
    }
}
