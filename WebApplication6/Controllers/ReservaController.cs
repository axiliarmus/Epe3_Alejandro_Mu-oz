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
    public class ReservaController : Controller
    {
        private readonly String StringConector;
        //aqui realizamos la conexion a base de datos 
        public ReservaController(IConfiguration config)
        {
            StringConector = config.GetConnectionString("MySqlConnection");
        }

        [HttpGet]
        //hacemos el httpget para que nos muestre todos los Reserva que estan en la lista
        public async Task<IActionResult> ListarReserva()
        {
            try
            {


                using (MySqlConnection conecta = new MySqlConnection(StringConector))
                {

                    await conecta.OpenAsync();

                    string sentencia = "SELECT * FROM Reserva";

                    List<Reserva> reservas = new List<Reserva>();

                    using (MySqlCommand comandos = new MySqlCommand(sentencia, conecta))


                    using (var lector = await comandos.ExecuteReaderAsync())
                    {

                        while (await lector.ReadAsync())
                        {

                            reservas.Add(new Reserva
                            {
                                id = lector.GetInt32(0),
                                Especialidad = lector.GetString(1),
                                DiaReserva = lector.GetDateTime(2),
                                Paciente_idPaciente = lector.GetInt32(3),


                            });


                        }

                        return StatusCode(200, reservas);

                    }

                }



            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo listar los registros por: " + ex);

            }
        }

        
        [HttpPost]
        // realizamos el metodo post para poder ingresar una nueva reserva 
        public async Task<IActionResult> GuardarMedicos([FromBody] Reserva reserva)
        {

            try
            {

                using (MySqlConnection conectar = new MySqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "INSERT INTO Reserva (id,Especialidad,DiaReserva, Paciente_idPaciente) VALUES (@id, @Especialidad, @DiaReserva, @Paciente_idPaciente)";

                    using (MySqlCommand comandos = new MySqlCommand(sentencia, conectar))
                    {

                        comandos.Parameters.Add(new MySqlParameter("@id", reserva.id));
                        comandos.Parameters.Add(new MySqlParameter("@Especialidad", reserva.Especialidad));
                        comandos.Parameters.Add(new MySqlParameter("@DiaReserva", reserva.DiaReserva));
                        comandos.Parameters.Add(new MySqlParameter("@Paciente_idPaciente", reserva.Paciente_idPaciente));

                        await comandos.ExecuteNonQueryAsync();

                        return StatusCode(201, $"Camion creada correctamente: {reserva}");

                    }

                }


            }
            catch (Exception ex)
            {

                return StatusCode(500, "No se pudo guardar el registro por :" + ex);

            }
        }


        [HttpPut("{id}")]
        //hacemos el metodo put para poder modifcar los datos de una Reserva 
        public async Task<IActionResult> EditarReserva(int id, [FromBody] Reserva reserva)
        {

            try
            {


                using (MySqlConnection conectar = new MySqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "UPDATE Reserva SET Especialidad = @Especialidad , DiaReserva = @DiaReserva, Paciente_idPaciente = @Paciente_idPaciente WHERE id = @id";

                    using (MySqlCommand comandos = new MySqlCommand(sentencia, conectar))
                    {
                        comandos.Parameters.Add(new MySqlParameter("@id", reserva.id));
                        comandos.Parameters.Add(new MySqlParameter("@Especialidad", reserva.Especialidad));
                        comandos.Parameters.Add(new MySqlParameter("@DiaReserva", reserva.DiaReserva));
                        comandos.Parameters.Add(new MySqlParameter("@Paciente_idPaciente", reserva.Paciente_idPaciente));


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

        // Metodo delete para poder borrar la reserva mediande buscarlo por la id
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPaciente(int id)
        {

            try
            {

                using (MySqlConnection conectar = new MySqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "DELETE FROM Reserva WHERE idReserva = @id";

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

                            return StatusCode(200, $"Reserva con el id {id} eliminada correctamente");

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
