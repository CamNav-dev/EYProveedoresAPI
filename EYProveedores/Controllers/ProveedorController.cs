using EYProveedores.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace EYProveedores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly string cadenaSQL;
        public ProveedorController(IConfiguration configuration)
        {
            cadenaSQL = configuration.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Proveedor> lista = new List<Proveedor>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.ListarProveedores", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Proveedor
                            {
                                IdProveedor = Convert.ToInt32(reader["Id_proveedor"]),
                                RazonSocial = reader["Razon_social"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                IdTributaria = Convert.ToInt32(reader["Id_tributaria"]),
                                Telefono = Convert.ToInt32(reader["Telefono"]),
                                Correo = reader["Correo"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                                Pais = reader["Pais"].ToString(),
                                SitioWeb = reader["Sitio_Web"].ToString(),
                                Facturacion = Convert.ToDecimal(reader["Facturacion"]),
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception error) { return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista }); }
        }

        [HttpGet]
        [Route("Buscar/{IdProveedor:int}")]
        public IActionResult Buscar(int IdProveedor)
        {
            Proveedor proveedor = null;
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.SeleccionarProveedor", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Id_proveedor", IdProveedor));

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            proveedor = new Proveedor
                            {
                                IdProveedor = Convert.ToInt32(reader["Id_proveedor"]),
                                RazonSocial = reader["Razon_social"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                IdTributaria = Convert.ToInt32(reader["Id_tributaria"]),
                                Telefono = Convert.ToInt32(reader["Telefono"]),
                                Correo = reader["Correo"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                                Pais = reader["Pais"].ToString(),
                                SitioWeb = reader["Sitio_Web"].ToString(),
                                Facturacion = Convert.ToDecimal(reader["Facturacion"]),
                            };
                        }
                    }
                }
                if (proveedor != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = proveedor });
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Proveedor no encontrado." });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult Guardar([FromBody] Proveedor objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.CrearProveedor", conexion);
                    cmd.Parameters.AddWithValue("Id_proveedor", objeto.IdProveedor);
                    cmd.Parameters.AddWithValue("Razon_social", objeto.RazonSocial);
                    cmd.Parameters.AddWithValue("Id_tributaria", objeto.IdTributaria);
                    cmd.Parameters.AddWithValue("Telefono", objeto.Telefono);
                    cmd.Parameters.AddWithValue("Correo", objeto.Correo);
                    cmd.Parameters.AddWithValue("Sitio_web", objeto.Correo);
                    cmd.Parameters.AddWithValue("Direccion", objeto.Direccion);
                    cmd.Parameters.AddWithValue("Pais", objeto.Pais);
                    cmd.Parameters.AddWithValue("Facturacion", objeto.Facturacion);
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
        public IActionResult Editar([FromBody] Proveedor objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.EditarProveedor", conexion);
                    cmd.Parameters.AddWithValue("Id_proveedor", objeto.IdProveedor);
                    cmd.Parameters.AddWithValue("Razon_social", objeto.RazonSocial);
                    cmd.Parameters.AddWithValue("Id_tributaria", objeto.IdTributaria);
                    cmd.Parameters.AddWithValue("Telefono", objeto.Telefono);
                    cmd.Parameters.AddWithValue("Correo", objeto.Correo);
                    cmd.Parameters.AddWithValue("Sitio_web", objeto.Correo);
                    cmd.Parameters.AddWithValue("Direccion", objeto.Direccion);
                    cmd.Parameters.AddWithValue("Pais", objeto.Pais);
                    cmd.Parameters.AddWithValue("Facturacion", objeto.Facturacion);
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
        [Route("Eliminar/{IdProveedor:int}")]
        public IActionResult Eliminar(int idProveedor)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.EliminarProveedor", conexion);
                    cmd.Parameters.AddWithValue("@Id_proveedor", idProveedor);
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
