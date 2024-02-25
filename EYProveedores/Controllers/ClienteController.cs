using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using EYProveedores.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
namespace EYProveedores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly string cadenaSQL;
        public ClienteController(IConfiguration configuration)
        {
            cadenaSQL = configuration.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.ListarCliente", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cliente
                            {
                                IdCliente = Convert.ToInt32(reader["Id_cliente"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Estado = reader["Estado"].ToString(),
                                Correo = reader["Correo"].ToString(),      
                                users = new Users {
                                    IdUser = Convert.ToInt32(reader["Id_user"]),
                                    Username = reader["Username"].ToString(),
                                    Password = reader["Password"].ToString()
                                },
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            } catch (Exception error) { return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista }); }
        }

        [HttpGet]
        [Route("Buscar/{IdCliente:int}")]
        public IActionResult Buscar(int IdCliente)
        {
            Cliente cliente = null;   
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.SeleccionarCliente", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Id_cliente", IdCliente));

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                IdCliente = Convert.ToInt32(reader["Id_cliente"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Estado = reader["Estado"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                users = new Users
                                {
                                    IdUser = Convert.ToInt32(reader["Id_user"]),
                                    Username = reader["Username"].ToString(),
                                    Password = reader["Password"].ToString()
                                }
                            };
                        }
                    }
                }
                if (cliente != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = cliente });
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Cliente no encontrado." });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult Guardar([FromBody] Cliente objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.CrearCliente", conexion); 

                    cmd.Parameters.AddWithValue("@Id_user", objeto.users.IdUser);

                    // Parámetros del Cliente
                    cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", objeto.Apellido);
                    cmd.Parameters.AddWithValue("@Estado", objeto.Estado);
                    cmd.Parameters.AddWithValue("@Correo", objeto.Correo);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "oki agregado" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Cliente objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.EditarCliente", conexion);
                    cmd.Parameters.AddWithValue("@Id_user", objeto.users.IdUser);

                    // Parámetros del Cliente
                    cmd.Parameters.AddWithValue("@Id_cliente", objeto.IdCliente);
                    cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", objeto.Apellido);
                    cmd.Parameters.AddWithValue("@Estado", objeto.Estado);
                    cmd.Parameters.AddWithValue("@Correo", objeto.Correo);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "oki editado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }

        [HttpDelete]
        [Route("Eliminar/{IdCliente:int}")]
        public IActionResult Eliminar(int idCliente)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.EliminarCliente", conexion);
                    cmd.Parameters.AddWithValue("@Id_cliente", idCliente);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "oki eliminado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }


    }
}
