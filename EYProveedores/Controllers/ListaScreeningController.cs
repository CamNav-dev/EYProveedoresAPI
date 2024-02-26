using EYProveedores.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace EYProveedores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaScreeningController : ControllerBase
    {
        private readonly string cadenaSQL;
        public ListaScreeningController(IConfiguration configuration)
        {
            cadenaSQL = configuration.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<ListaScreening> lista = new List<ListaScreening>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.ListarListaScreening", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ListaScreening
                            {
                                IdScreening = Convert.ToInt32(reader["Id_screening"]),
                                Fecha = Convert.ToDateTime(reader["Fecha"]),
                                Tipo = reader["Tipo"].ToString(),
                                IdCliente = Convert.ToInt32(reader["Id_cliente"]),
                                IdOfac = Convert.ToInt32(reader["Id_ofac"]),
                                IdOs = Convert.ToInt32(reader["Id_os"]),
                                IdWbs = Convert.ToInt32(reader["Id_wbs"]),
                                IdProveedor = Convert.ToInt32(reader["Id_proveedor"]),
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception error) { return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista }); }
        }

        [HttpGet]
        [Route("Buscar/{IdScreening:int}")]
        public IActionResult Buscar(int IdScreening)
        {
            ListaScreening listaScreening = null;
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.SeleccionarListaScreening", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Id_screening", IdScreening));

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            listaScreening = new ListaScreening
                            {
                                IdScreening = Convert.ToInt32(reader["Id_screening"]),
                                Fecha = Convert.ToDateTime(reader["Fecha"]),
                                Tipo = reader["Tipo"].ToString(),
                                IdCliente = Convert.ToInt32(reader["Id_cliente"]),
                                IdOfac = Convert.ToInt32(reader["Id_ofac"]),
                                IdOs = Convert.ToInt32(reader["Id_os"]),
                                IdWbs = Convert.ToInt32(reader["Id_wbs"]),
                                IdProveedor = Convert.ToInt32(reader["Id_proveedor"]),

                            };
                        }
                    }
                }
                if (listaScreening != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = listaScreening });
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Lista Screening no encontrada." });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult Crear([FromBody] ListaScreening objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.CrearListaScreening", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Asignar valores a los parámetros del procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@Id_screening", objeto.IdScreening));
                    cmd.Parameters.Add(new SqlParameter("@Fecha", objeto.Fecha));
                    cmd.Parameters.Add(new SqlParameter("@Tipo", objeto.Tipo));
                    cmd.Parameters.Add(new SqlParameter("@Id_proveedor", objeto.IdProveedor));
                    cmd.Parameters.Add(new SqlParameter("@Id_os", objeto.IdOs));
                    cmd.Parameters.Add(new SqlParameter("@Id_wbs", objeto.IdWbs));
                    cmd.Parameters.Add(new SqlParameter("@Id_ofac", objeto.IdOfac));
                    cmd.Parameters.Add(new SqlParameter("@Id_cliente", objeto.IdCliente));

                    // Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "oki agregado." });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] ListaScreening objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.EditarListaScreening", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Asignar valores a los parámetros del procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@Id_screening", objeto.IdScreening));
                    cmd.Parameters.Add(new SqlParameter("@Fecha", objeto.Fecha));
                    cmd.Parameters.Add(new SqlParameter("@Tipo", objeto.Tipo));
                    cmd.Parameters.Add(new SqlParameter("@Id_proveedor", objeto.IdProveedor));
                    cmd.Parameters.Add(new SqlParameter("@Id_os", objeto.IdOs));
                    cmd.Parameters.Add(new SqlParameter("@Id_wbs", objeto.IdWbs));
                    cmd.Parameters.Add(new SqlParameter("@Id_ofac", objeto.IdOfac));
                    cmd.Parameters.Add(new SqlParameter("@Id_cliente", objeto.IdCliente));

                    // Ejecutar el procedimiento almacenado
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
        [Route("Eliminar/{IdScreening:int}")]
        public IActionResult Eliminar(int idScreening)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EYProveedores.EliminarListaScreening", conexion);
                    cmd.Parameters.AddWithValue("@Id_screening", idScreening);
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
