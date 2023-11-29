using API_RESERVA.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;



namespace API_RESERVA.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly string StringConector;

        public PacienteController(IConfiguration config)
        {
            StringConector = config.GetConnectionString("MySqlConnection");
        }

        [HttpGet]
        public IActionResult ListarPacientes()
        {
            try
            {
                using (MySqlConnection conecta = new MySqlConnection(StringConector))
                {
                    conecta.Open();

                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Paciente", conecta))
                    {
                        MySqlDataReader reader = command.ExecuteReader();
                        var pacientes = new List<Paciente>();
                        while (reader.Read())
                        {
                            pacientes.Add(new Paciente()
                            {
                                id_pac = reader.GetInt32(0),
                                nombre_pac = reader.GetString(1),
                                apellido_pac = reader.GetString(2),
                                run_pac = reader.GetString(3),
                                nacionalidad_pac = reader.GetString(4),
                                visa = reader.GetString(5),
                                genero = reader.GetString(6),
                                sintomas_pac = reader.GetString(7),
                                medico_id_med = reader.GetInt32(8)
                            });
                        }

                        return StatusCode(200, pacientes);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha podido listar los pacientes" + ex);
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

                    var sentencia = "SELECT * FROM Paciente WHERE IdPaciente = idPaciente";

                    using (MySqlCommand command = new MySqlCommand(sentencia, conecta))
                    {
                        command.Parameters.Add(new MySqlParameter("idPaciente", id));
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            var pacientes = new Paciente()
                            {
                                id_pac = reader.GetInt32(0),
                                nombre_pac = reader.GetString(1),
                                apellido_pac = reader.GetString(2),
                                run_pac = reader.GetString(3),
                                nacionalidad_pac = reader.GetString(4),
                                visa = reader.GetString(5),
                                genero = reader.GetString(6),
                                sintomas_pac = reader.GetString(7),
                                medico_id_med = reader.GetInt32(8)
                            };
                            return StatusCode(200, pacientes);
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
        public IActionResult GuardarPacientes([FromBody] Paciente pacientes)
        {
            try
            {

                using (MySqlConnection conector = new MySqlConnection(StringConector))
                {

                    conector.Open();

                    string sentencia = "INSERT INTO Paciente (idPaciente,NombrePac, ApellidoPac, RunPac, Nacionalidad, Visa, Genero, SintomasPac, Medico_idMedico) VALUES (@idPaciente, @NombrePac, @ApellidoPac, @RunPac, @Nacionalidad, @Visa, @Genero, @SintomasPac, @Medico_idMedico)";

                    MySqlCommand comando = new MySqlCommand(sentencia, conector);


                    comando.Parameters.Add(new MySqlParameter("idPaciente", pacientes.id_pac));
                    comando.Parameters.Add(new MySqlParameter("NombrePac", pacientes.nombre_pac));
                    comando.Parameters.Add(new MySqlParameter("ApellidoPac", pacientes.apellido_pac));
                    comando.Parameters.Add(new MySqlParameter("RunPac", pacientes.run_pac));
                    comando.Parameters.Add(new MySqlParameter("Nacionalidad", pacientes.nacionalidad_pac));
                    comando.Parameters.Add(new MySqlParameter("Visa", pacientes.visa));
                    comando.Parameters.Add(new MySqlParameter("Genero", pacientes.genero));
                    comando.Parameters.Add(new MySqlParameter("SintomasPac", pacientes.sintomas_pac));
                    comando.Parameters.Add(new MySqlParameter("Medico_idMedico", pacientes.medico_id_med));


                    comando.ExecuteNonQuery();

                    return StatusCode(201, "Paciente creado exitosamente");


                }

            }
            catch (Exception ex)

            {
                return StatusCode(500, "No se pudo guardar el registro por: " + ex);

            }
        }

        [HttpPost("{id}")]
        public IActionResult EditarPacientes(int id, [FromBody] Paciente pacientes)
        {
            try
            {


                using (MySqlConnection conector = new MySqlConnection(StringConector))
                {

                    conector.Open();

                    string sentencia = "UPDATE Paciente SET NombrePac = @NombrePac , ApellidoPac= @ApellidoPac, RunPac = @RunPac, Nacionalidad = @Nacionalidad , Visa = @Visa, Genero = @Genero, SintomasPac = @SintomasPac, Medico_idMedico = @Medico_idMedico WHERE idPaciente = @idPaciente";

                    using (MySqlCommand comando = new MySqlCommand(sentencia, conector))
                    {

                        comando.Parameters.Add(new MySqlParameter("NombrePac", pacientes.nombre_pac));
                        comando.Parameters.Add(new MySqlParameter("ApellidoPac", pacientes.apellido_pac));
                        comando.Parameters.Add(new MySqlParameter("RunPac", pacientes.run_pac));
                        comando.Parameters.Add(new MySqlParameter("Nacionalidad", pacientes.nacionalidad_pac));
                        comando.Parameters.Add(new MySqlParameter("Visa", pacientes.visa));
                        comando.Parameters.Add(new MySqlParameter("Genero", pacientes.genero));
                        comando.Parameters.Add(new MySqlParameter("SintomasPac", pacientes.sintomas_pac));
                        comando.Parameters.Add(new MySqlParameter("Medico_idMedico", pacientes.medico_id_med));
                        comando.Parameters.Add(new MySqlParameter("idMedico", id));

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
        public ActionResult EliminarPacientes(int id)
        {
            try
            {

                using (MySqlConnection conector = new MySqlConnection(StringConector))
                {

                    conector.Open();

                    string sentencia = "DELETE FROM Paciente WHERE idPaciente = @idPaciente";

                    using (MySqlCommand comando = new MySqlCommand(sentencia, conector))
                    {

                        comando.Parameters.Add(new MySqlParameter("idPaciente", id));

                        int filas = comando.ExecuteNonQuery();

                        if (filas == 0)
                        {

                            return StatusCode(404, "Registro no encontrado");

                        }
                        else
                        {

                            return StatusCode(200, $"Persona con el IdPaciente {id} ha sido eliminada exitosamente");

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


