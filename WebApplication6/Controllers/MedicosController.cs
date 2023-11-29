using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.Model;


namespace WebApplication6.Controllers
{
    [Route("[controller]")]
    public class MedicosController : Controller
    {
        private readonly String StringConector;
        //aqui realizamos la conexion a base de datos 
        public MedicosController(IConfiguration config)
        {
            StringConector = config.GetConnectionString("MySqlConnection");
        }
        

        [HttpGet]
        //hacemos el httpget para que nos muestre todos los camioneros que estan en la lista
        public async Task<IActionResult> ListarMedicos()
        {
            try
            {


                using (MySqlConnection conecta = new MySqlConnection(StringConector))
                {

                    await conecta.OpenAsync();

                    string sentencia = "SELECT * FROM Medico";

                    List<Medicos> Medico = new List<Medicos>();

                    using (MySqlCommand comandos = new MySqlCommand(sentencia, conecta))


                    using (var lector = await comandos.ExecuteReaderAsync())
                    {

                        while (await lector.ReadAsync())
                        {

                            Medico.Add(new Medicos
                            {
                                id = lector.GetInt32(0),
                                NombreMed = lector.GetString(1),
                                ApellidoMed = lector.GetString(2),
                                RunMed = lector.GetString(3),
                                Eunacom = lector.GetString(4),
                                NacionalidadMed = lector.GetString(5),
                                Especialidad = lector.GetString(6),
                                Horarios = lector.GetDateTime(7),
                                TarifaHr = lector.GetInt32(8)

                            });


                        }

                        return StatusCode(200, Medico);

                    }

                }



            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo listar los registros por: " + ex);

            }
        }

        [HttpGet("{id}")]
        // aqui pedimos el id del medico para poder traer a un medico en especifico
        public async Task<IActionResult> ListarMedicos(int id)
        {
            try
            {

                using (MySqlConnection conectar = new MySqlConnection(StringConector))
                {


                    await conectar.OpenAsync();

                    string sentencia = "SELECT * FROM Medico WHERE id = ?";

                    Medicos Medico = new Medicos();

                    using (MySqlCommand comandos = new MySqlCommand(sentencia, conectar))
                    {

                        comandos.Parameters.Add(new MySqlParameter("@id", id));

                        using (var lector = await comandos.ExecuteReaderAsync())
                        {


                            if (await lector.ReadAsync())
                            {
                                Medico.id = lector.GetInt32(0);
                                Medico.NombreMed = lector.GetString(1);
                                Medico.ApellidoMed = lector.GetString(2);
                                Medico.RunMed = lector.GetString(3);
                                Medico.Eunacom = lector.GetString(4);
                                Medico.NacionalidadMed = lector.GetString(5);
                                Medico.Especialidad = lector.GetString(6);
                                Medico.Horarios = lector.GetDateTime(7);
                                Medico.TarifaHr = lector.GetInt32(8);
                                return StatusCode(200, Medico);

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
        // realizamos el metodo post para poder ingresar un nuevo medico 
        public async Task<IActionResult> GuardarMedicos([FromBody] Medicos medicos)
        {

            try
            {

                using (MySqlConnection conectar = new MySqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "INSERT INTO medico (id,NombreMed,ApellidoMed, RunMed, Eunacom, NacionalidadMed, Especialidad,Horarios,TarifaHr ) VALUES (@id, @NombreMed, @ApellidoMed, @RunMed, @Eunacom, @NacionalidadMed, @Especialidad, @Horarios,@TarifaHr)";

                    using (MySqlCommand comandos = new MySqlCommand(sentencia, conectar))
                    {

                        comandos.Parameters.Add(new MySqlParameter("@id", medicos.id));
                        comandos.Parameters.Add(new MySqlParameter("@NombreMed", medicos.NombreMed));
                        comandos.Parameters.Add(new MySqlParameter("@ApellidoMed", medicos.ApellidoMed));
                        comandos.Parameters.Add(new MySqlParameter("@RunMed", medicos.RunMed));
                        comandos.Parameters.Add(new MySqlParameter("@Eunacom", medicos.Eunacom));
                        comandos.Parameters.Add(new MySqlParameter("@NacionalidadMed", medicos.NacionalidadMed));
                        comandos.Parameters.Add(new MySqlParameter("@Especialidad", medicos.Especialidad));
                        comandos.Parameters.Add(new MySqlParameter("@Horarios", medicos.Horarios));
                        comandos.Parameters.Add(new MySqlParameter("@TarifaHr", medicos.TarifaHr));

                        await comandos.ExecuteNonQueryAsync();

                        return StatusCode(201, $"Camion creada correctamente: {medicos}");

                    }

                }


            }
            catch (Exception ex)
            {

                return StatusCode(500, "No se pudo guardar el registro por :" + ex);

            }
        }


        [HttpPut("{id}")]
        //hacemos el metodo put para poder modifcar los datos de un medico 
        public async Task<IActionResult> EditarMedico(int id, [FromBody] Medicos medicos)
        {

            try
            {


                using (MySqlConnection conectar = new MySqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "UPDATE Medico SET NombreMed = @NombreMed , ApellidoMed = @ApellidoMed, RunMed = @RunMed, Eunacom = @Eunacom , NacionalidadMed = @NacionalidadMed, Especialidad = @Especialidad, Horarios = @Horarios, TarifaHr = @TarifaHr WHERE id = @id";

                    using (MySqlCommand comandos = new MySqlCommand(sentencia, conectar))
                    {
                        comandos.Parameters.Add(new MySqlParameter("@id", medicos.id));
                        comandos.Parameters.Add(new MySqlParameter("@NombreMed", medicos.NombreMed));
                        comandos.Parameters.Add(new MySqlParameter("@ApellidoMed", medicos.ApellidoMed));
                        comandos.Parameters.Add(new MySqlParameter("@RunMed", medicos.RunMed));
                        comandos.Parameters.Add(new MySqlParameter("@Eunacom", medicos.Eunacom));
                        comandos.Parameters.Add(new MySqlParameter("@NacionalidadMed", medicos.NacionalidadMed));
                        comandos.Parameters.Add(new MySqlParameter("@Especialidad", medicos.Especialidad));
                        comandos.Parameters.Add(new MySqlParameter("@Horarios", medicos.Horarios));
                        comandos.Parameters.Add(new MySqlParameter("@TarifaHr", medicos.TarifaHr));


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

        // Metodo delete para poder borrar el medico mediande buscarlo por la id
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMedico(int id)
        {

            try
            {

                using (MySqlConnection conectar = new MySqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "DELETE FROM Medico WHERE id = @id";

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

                            return StatusCode(200, $"Medico con el id {id} eliminada correctamente");

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
