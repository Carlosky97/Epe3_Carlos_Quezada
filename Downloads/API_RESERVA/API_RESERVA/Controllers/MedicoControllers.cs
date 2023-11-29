using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using API_RESERVA.Models;

namespace API_RESERVA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly string StringConector;

        public MedicoController(IConfiguration config)
        {
            StringConector = config.GetConnectionString("MySqlConnection");
        }

        [HttpGet]
        public IActionResult ListarMedicos()
        {
            try
            {
                using (MySqlConnection conecta = new MySqlConnection(StringConector))
                {
                    conecta.Open();

                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Medico", conecta))
                    {
                        MySqlDataReader reader = command.ExecuteReader();
                        var medicos = new List<Medico>();
                        while (reader.Read())
                        {
                            medicos.Add(new Medico
                            {
                                id_med = reader.GetInt32(0),
                                nombre_med = reader.GetString(1),
                                apellido_med = reader.GetString(2),
                                run_med = reader.GetString(3),
                                eunacom = reader.GetString(4),
                                nacionalidad_med = reader.GetString(5),
                                especialidad = reader.GetString(6),
                                horarios = reader.GetString(7),
                                tarifahr = reader.GetInt32(8)
                            });
                        }

                        return StatusCode(200, medicos);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha podido listar los medicos" + ex);
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

                    var sentencia = "SELECT * FROM Medico WHERE IdMedico = idMedico";

                    using (MySqlCommand command = new MySqlCommand(sentencia, conecta))
                    {
                        command.Parameters.Add(new MySqlParameter("idMedicos", id)); 
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            var medicos = new Medico()
                            {
                                id_med = reader.GetInt32(0),
                                nombre_med = reader.GetString(1),
                                apellido_med = reader.GetString(2),
                                run_med = reader.GetString(3),
                                eunacom = reader.GetString(4),
                                nacionalidad_med = reader.GetString(5),
                                especialidad = reader.GetString(6),
                                horarios = reader.GetString(7),
                                tarifahr = reader.GetInt32(8)
                            };
                            return StatusCode(200, medicos);
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
        public IActionResult GuardarMedicos([FromBody] Medico medicos)
        {
            try
            {

                using (MySqlConnection conector = new MySqlConnection(StringConector))
                {

                    conector.Open();

                    string sentencia = "INSERT INTO Medico (idMedico,NombreMed, ApellidoMed, RunMed, Eunacom, NacionalidadMed, Especialidad, Horarios, TarifaHr) VALUES (@idMedico, @NombreMed, @ApellidoMed, @RunMed, @Eunacom, @NacionalidadMed, @Especialidad, @Horarios, @TarifaHr)";

                    MySqlCommand comando = new MySqlCommand(sentencia, conector);


                    comando.Parameters.Add(new MySqlParameter("idMedico", medicos.id_med));
                    comando.Parameters.Add(new MySqlParameter("NombreMed", medicos.nombre_med));
                    comando.Parameters.Add(new MySqlParameter("ApellidoMed", medicos.apellido_med));
                    comando.Parameters.Add(new MySqlParameter("RunMed", medicos.run_med));
                    comando.Parameters.Add(new MySqlParameter("Eunacom", medicos.eunacom));
                    comando.Parameters.Add(new MySqlParameter("NacionalidadMed", medicos.nacionalidad_med));
                    comando.Parameters.Add(new MySqlParameter("Especialidad", medicos.especialidad));
                    comando.Parameters.Add(new MySqlParameter("Horarios", medicos.horarios));
                    comando.Parameters.Add(new MySqlParameter("TarifaHr", medicos.tarifahr));


                    comando.ExecuteNonQuery();

                    return StatusCode(201, "Medico creado correctamente");


                }

            }
            catch (Exception ex)

            {
                return StatusCode(500, "No se pudo guardar el registro por: " + ex);

            }
        }

        [HttpPost("{id}")]
        public IActionResult EditarMedico(int id, [FromBody] Medico medicos)
        {
            try
            {


                using (MySqlConnection conector = new MySqlConnection(StringConector))
                {

                    conector.Open();

                    string sentencia = "UPDATE Medico SET NombreMed = @NombreMed , ApellidoMed= @ApellidoMed, RunMed = @RunMed, Eunacom = @Eunacom , NacionalidadMed = @NacionalidadMed, Especialidad = @Especialidad, Horarios = @Horarios, TarifaHr = @TarifaHr WHERE idMedico = @idMedico";

                    using (MySqlCommand comando = new MySqlCommand(sentencia, conector))
                    {

                        comando.Parameters.Add(new MySqlParameter("NombreMed", medicos.nombre_med));
                        comando.Parameters.Add(new MySqlParameter("ApellidoMed", medicos.apellido_med));
                        comando.Parameters.Add(new MySqlParameter("RunMed", medicos.run_med));
                        comando.Parameters.Add(new MySqlParameter("Eunacom", medicos.eunacom));
                        comando.Parameters.Add(new MySqlParameter("NacionalidadMed", medicos.nacionalidad_med));
                        comando.Parameters.Add(new MySqlParameter("Especialidad", medicos.especialidad));
                        comando.Parameters.Add(new MySqlParameter("Horarios",medicos.horarios));
                        comando.Parameters.Add(new MySqlParameter("TarifaHr", medicos.tarifahr));
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
        public ActionResult EliminarMedico(int id)
        {
            try
            {

                using (MySqlConnection conector = new MySqlConnection(StringConector))
                {

                    conector.Open();

                    string sentencia = "DELETE FROM Medico WHERE idMedico = @idMedico";

                    using (MySqlCommand comando = new MySqlCommand(sentencia, conector))
                    {

                        comando.Parameters.Add(new MySqlParameter("idMedico", id));

                        int filas = comando.ExecuteNonQuery();

                        if (filas == 0)
                        {

                            return StatusCode(404, "Registro no encontrado");

                        }
                        else
                        {

                            return StatusCode(200, $"Persona con el IdMedico {id} ha sido eliminada correctamente");

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

