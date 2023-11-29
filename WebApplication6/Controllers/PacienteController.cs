using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.Model;
using System.Data;

namespace WebApplication6.Controllers
{
    [Route("[controller]")]
    public class PacienteController : Controller
    {
        private readonly String StringConector;
        //aqui realizamos la conexion a base de datos 
        public PacienteController(IConfiguration config)
        {
            StringConector = config.GetConnectionString("MySqlConnection");
        }

        [HttpGet]
        //hacemos el httpget para que nos muestre todos los Paciente que estan en la lista
        public async Task<IActionResult> ListarPaciente()
        {
            try
            {


                using (MySqlConnection conecta = new MySqlConnection(StringConector))
                {

                    await conecta.OpenAsync();

                    string sentencia = "SELECT * FROM Paciente";

                    List<Pacientes> Paciente = new List<Pacientes>();

                    using (MySqlCommand comandos = new MySqlCommand(sentencia, conecta))


                    using (var lector = await comandos.ExecuteReaderAsync())
                    {

                        while (await lector.ReadAsync())
                        {

                            Paciente.Add(new Pacientes
                            {
                                id = lector.GetInt32(0),
                                NombrePac = lector.GetString(1),
                                ApellidoPac = lector.GetString(2),
                                RunPac = lector.GetString(3),
                                Nacionalidad = lector.GetString(4),
                                Visa = lector.GetString(5),
                                Genero = lector.GetString(6),
                                SintomasPac = lector.GetString(7),
                                ocho = lector.GetInt32(8),
                                Medico_idMedico = lector.GetInt32(9)

                            });


                        }

                        return StatusCode(200, Paciente);

                    }

                }



            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo listar los registros por: " + ex);

            }
        }

        [HttpGet("{id}")]
        // aqui pedimos el id del medico para poder traer a un paciente en especifico
        public async Task<IActionResult> ListarPacientes(int id)
        {
            try
            {

                using (MySqlConnection conectar = new MySqlConnection(StringConector))
                {


                    await conectar.OpenAsync();

                    string sentencia = "SELECT * FROM Paciente WHERE idPaciente = ?";

                    Pacientes Paciente = new Pacientes();

                    using (MySqlCommand comandos = new MySqlCommand(sentencia, conectar))
                    {

                        comandos.Parameters.Add(new MySqlParameter("@id", id));

                        using (var lector = await comandos.ExecuteReaderAsync())
                        {


                            if (await lector.ReadAsync())
                            {
                                Paciente.id = lector.GetInt32(0);
                                Paciente.NombrePac = lector.GetString(1);
                                Paciente.ApellidoPac = lector.GetString(2);
                                Paciente.RunPac = lector.GetString(3);
                                Paciente.Nacionalidad = lector.GetString(4);
                                Paciente.Visa = lector.GetString(5);
                                Paciente.Genero = lector.GetString(6);
                                Paciente.SintomasPac = lector.GetString(7);
                                Paciente.ocho = lector.GetInt32(8);
                                Paciente.Medico_idMedico = lector.GetInt32(8);
                                return StatusCode(200, Paciente);

                            }
                            else
                            {

                                return StatusCode(404, "No se encuentra el registro");

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "No se puede realizar la peticion por: " + ex);
            }
        }


        [HttpPost]
        // realizamos el metodo post para poder ingresar un nuevo paciente 
        public async Task<IActionResult> GuardarMedicos([FromBody] Pacientes paciente)
        {

            try
            {

                using (MySqlConnection conectar = new MySqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "INSERT INTO Peciente (id,NombrePac,ApellidoPac, RunPac, Nacionalidad, Visa, Genero,SintomasPac,ocho, Medico_idMedico ) VALUES (@id, @NombrePac, @ApellidoPac, @RunPac, @Nacionalidad, @Visa, @Genero, @SintomasPac,@ocho, @Medico_idMedico)";

                    using (MySqlCommand comandos = new MySqlCommand(sentencia, conectar))
                    {

                        comandos.Parameters.Add(new MySqlParameter("@id", paciente.id));
                        comandos.Parameters.Add(new MySqlParameter("@NombrePac", paciente.NombrePac));
                        comandos.Parameters.Add(new MySqlParameter("@ApellidoPac", paciente.ApellidoPac));
                        comandos.Parameters.Add(new MySqlParameter("@RunPac", paciente.RunPac));
                        comandos.Parameters.Add(new MySqlParameter("@Nacionalidad", paciente.Nacionalidad));
                        comandos.Parameters.Add(new MySqlParameter("@Visa", paciente.Visa));
                        comandos.Parameters.Add(new MySqlParameter("@Genero", paciente.Genero));
                        comandos.Parameters.Add(new MySqlParameter("@SintomasPac", paciente.SintomasPac));
                        comandos.Parameters.Add(new MySqlParameter("@ocho", paciente.ocho));
                        comandos.Parameters.Add(new MySqlParameter("@Medico_idMedico", paciente.Medico_idMedico));

                        await comandos.ExecuteNonQueryAsync();

                        return StatusCode(201, $"Camion creada correctamente: {paciente}");

                    }

                }


            }
            catch (Exception ex)
            {

                return StatusCode(500, "No se pudo guardar el registro por :" + ex);

            }
        }


        [HttpPut("{id}")]
        //hacemos el metodo put para poder modifcar los datos de un Paciente 
        public async Task<IActionResult> EditarMedico(int id, [FromBody] Pacientes paciente)
        {

            try
            {


                using (MySqlConnection conectar = new MySqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "UPDATE Paciente SET NombrePac = @NombrePac , ApellidoPac = @ApellidoPac, RunPac = @RunPac, Nacionalidad = @Nacionalidad , Visa = @Visa, Genero = @Genero, SintomasPac = @SintomasPac, ocho = @ocho,Medico_idMedico = @Medico_idMedico  WHERE idPaciente = @id";

                    using (MySqlCommand comandos = new MySqlCommand(sentencia, conectar))
                    {
                        comandos.Parameters.Add(new MySqlParameter("@id", paciente.id));
                        comandos.Parameters.Add(new MySqlParameter("@NombrePac", paciente.NombrePac));
                        comandos.Parameters.Add(new MySqlParameter("@ApellidoPac", paciente.ApellidoPac));
                        comandos.Parameters.Add(new MySqlParameter("@RunPac", paciente.RunPac));
                        comandos.Parameters.Add(new MySqlParameter("@Nacionalidad", paciente.Nacionalidad));
                        comandos.Parameters.Add(new MySqlParameter("@Visa", paciente.Visa));
                        comandos.Parameters.Add(new MySqlParameter("@Genero", paciente.Genero));
                        comandos.Parameters.Add(new MySqlParameter("@SintomasPac", paciente.SintomasPac));
                        comandos.Parameters.Add(new MySqlParameter("@ocho", paciente.ocho));
                        comandos.Parameters.Add(new MySqlParameter("@Medico_idMedico", paciente.Medico_idMedico));


                        await comandos.ExecuteNonQueryAsync();

                        return StatusCode(200, "Registro editado correctamente");

                    }

                }

            }
            catch (Exception ex)
            {

                return StatusCode(500, "No se pudo editar la persona por :" + ex);

            }

        }

        // Metodo delete para poder borrar el Paciente mediande buscarlo por la id
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPaciente(int id)
        {

            try
            {

                using (MySqlConnection conectar = new MySqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "DELETE FROM Paciente WHERE idPaciente = @id";

                    using (MySqlCommand comandos = new MySqlCommand(sentencia, conectar))
                    {

                        comandos.Parameters.Add(new MySqlParameter("@id", id));

                        var borrado = await comandos.ExecuteNonQueryAsync();

                        if (borrado == 0)
                        {

                            return StatusCode(404, "Registro no encontrado!!!");


                        }
                        else
                        {

                            return StatusCode(200, $"Paciente con el id {id} eliminada correctamente");

                        }

                    }

                }


            }
            catch (Exception ex)
            {

                return StatusCode(500, "No se pudo eliminar el registro por: " + ex);

            }
        }
    }
}
