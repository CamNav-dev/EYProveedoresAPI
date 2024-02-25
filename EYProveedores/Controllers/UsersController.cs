using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using EYProveedores.Models;
namespace EYProveedores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly string cadenaSQL;
        public UsersController(IConfiguration configuration)
        {
            cadenaSQL = configuration.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Users> lista = new List<Users>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.ListarUsuario", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Users
                            {
                                IdUser = Convert.ToInt32(reader["Id_user"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString()
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception error) { return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista }); }
        }

        [HttpGet]
        [Route("Buscar/{IdUser:int}")]
        public IActionResult Buscar(int IdUser)
        {
            Users user = null;
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.SeleccionarUsuario", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Id_user", IdUser));

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {
                            user = new Users
                            {
                                IdUser = Convert.ToInt32(reader["Id_user"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString()
                            };
                        }
                    }
                }
                if (user != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = user });
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Usuario no encontrado." });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult Guardar([FromBody] Users objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.CrearUsuario", conexion);
                    cmd.Parameters.AddWithValue("Id_user", objeto.IdUser);
                    cmd.Parameters.AddWithValue("Username", objeto.Username);
                    cmd.Parameters.AddWithValue("Password", objeto.Password);
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
        public IActionResult Editar([FromBody] Users objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.EditarUsuario", conexion);
                    cmd.Parameters.AddWithValue("Id_user", objeto.IdUser);
                    cmd.Parameters.AddWithValue("Username", objeto.Username);
                    cmd.Parameters.AddWithValue("Password", objeto.Password);
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
        [Route("Eliminar/{IdUser:int}")]
        public IActionResult Eliminar(int idUser)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.EliminarUsuario", conexion);
                    cmd.Parameters.AddWithValue("@Id_user", idUser);
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