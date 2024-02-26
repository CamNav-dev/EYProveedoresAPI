using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using EYProveedores.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using EYProveedores.Models.Custom;
using EYProveedores.Services;
using Microsoft.Extensions.Configuration;
namespace EYProveedores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Services.IAuthorizationService _authorizationService;
        private readonly string cadenaSQL;
        public UsersController(Services.IAuthorizationService authorizationService, IConfiguration configuration)
        {
            _authorizationService = authorizationService;
            cadenaSQL = configuration.GetConnectionString("CadenaSQL");
        }

        //Authenticate Endpoint

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Autenticar([FromBody] AuthorizationRequest authorization)
        {
            var resultado_authorization = await _authorizationService.DevolverToken(authorization);
            if (resultado_authorization == null)
                return Unauthorized();
            return Ok(resultado_authorization);
        }   




        //CRUD Endpoint
    
        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<User> lista = new List<User>();
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
                            lista.Add(new User
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
            User user = null;
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
                            user = new User
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
        public IActionResult Guardar([FromBody] User objeto)
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
        public IActionResult Editar([FromBody] User objeto)
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