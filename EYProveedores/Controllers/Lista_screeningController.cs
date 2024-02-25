using EYProveedores.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace EYProveedores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Lista_screeningController : ControllerBase
    {
        private readonly string cadenaSQL;
        public Lista_screeningController(IConfiguration configuration)
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
                                proveedor = new Proveedor
                                {
                                    IdProveedor = Convert.ToInt32(reader["Id_proveedor"]),
                                    RazonSocial = reader["Proveedor_Razon_Social"].ToString(),
                                    Nombre = reader["Proveedor_Nombre"].ToString(),
                                    IdTributaria = Convert.ToInt32(reader["Id_tributaria"]),
                                    Telefono = Convert.ToInt32(reader["Telefono"]),
                                    Correo = reader["Proveedor_Correo"].ToString(),
                                    Direccion = reader["Proveedor_Direccion"].ToString(),
                                    Pais = reader["Proveedor_Pais"].ToString(),
                                    Facturacion = Convert.ToDecimal(reader["Facturacion"]),
                                },
                                offacSource = new OFFACSource
                                {
                                    IdOfac = Convert.ToInt32(reader["Id_ofac"]),
                                    Name = reader["OFAC_Name"].ToString(),
                                    Address = reader["OFAC_Address"].ToString(),
                                    Type = reader["OFAC_Type"].ToString(),
                                    Programs = reader["Programs"].ToString(),
                                    List = reader["List"].ToString(),
                                    Score = reader["Score"].ToString()
                                },

                                theWorldBankSource = new TheWorldBankSource
                                {
                                    IdWbs = Convert.ToInt32(reader["Id_wbs"]),
                                    Firm_name = reader["WBS_Firm_Name"].ToString(),
                                    Address = reader["WBS_Address"].ToString(),
                                    Country = reader["WBS_Country"].ToString(),
                                    From_date = Convert.ToDateTime(reader["From_date"]),
                                    To_date = Convert.ToDateTime(reader["To_date"]),
                                    Grounds = reader["Grounds"].ToString()
                                },

                                offshore = new OffshoreSource
                                {
                                    IdOs = Convert.ToInt32(reader["Id_os"]),
                                    Entity = reader["Offshore_Entity"].ToString(),
                                    Jurisdiction = reader["Jurisdiction"].ToString(),
                                    LinkedTo = reader["Linked_to"].ToString(),
                                    DataFrom = reader["Data_from"].ToString()
                                },

                                cliente = new Cliente
                                {
                                    IdCliente = Convert.ToInt32(reader["Id_cliente"]),
                                    Nombre = reader["Cliente_Nombre"].ToString(),
                                    Apellido = reader["Cliente_Apellido"].ToString(),
                                    Estado = reader["Cliente_Estado"].ToString(),
                                    Correo = reader["Cliente_Correo"].ToString(),
                                    users = new Users
                                    {
                                        IdUser = Convert.ToInt32(reader["Id_user"]),
                                        Username = reader["Username"].ToString(),
                                        Password = reader["Password"].ToString()
                                    }
                                },
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
                                proveedor = new Proveedor
                                {
                                    IdProveedor = Convert.ToInt32(reader["Id_proveedor"]),
                                    RazonSocial = reader["Proveedor_Razon_Social"].ToString(),
                                    Nombre = reader["Proveedor_Nombre"].ToString(),
                                    IdTributaria = Convert.ToInt32(reader["Id_tributaria"]),
                                    Telefono = Convert.ToInt32(reader["Telefono"]),
                                    Correo = reader["Proveedor_Correo"].ToString(),
                                    Direccion = reader["Proveedor_Direccion"].ToString(),
                                    Pais = reader["Proveedor_Pais"].ToString(),
                                    Facturacion = Convert.ToDecimal(reader["Facturacion"]),
                                },
                                offacSource = new OFFACSource
                                {
                                    IdOfac = Convert.ToInt32(reader["Id_ofac"]),
                                    Name = reader["OFAC_Name"].ToString(),
                                    Address = reader["OFAC_Address"].ToString(),
                                    Type = reader["OFAC_Type"].ToString(),
                                    Programs = reader["Programs"].ToString(),
                                    List = reader["List"].ToString(),
                                    Score = reader["Score"].ToString()
                                },

                                theWorldBankSource = new TheWorldBankSource
                                {
                                    IdWbs = Convert.ToInt32(reader["Id_wbs"]),
                                    Firm_name = reader["WBS_Firm_Name"].ToString(),
                                    Address = reader["WBS_Address"].ToString(),
                                    Country = reader["WBS_Country"].ToString(),
                                    From_date = Convert.ToDateTime(reader["From_date"]),
                                    To_date = Convert.ToDateTime(reader["To_date"]),
                                    Grounds = reader["Grounds"].ToString()
                                },

                                offshore = new OffshoreSource
                                {
                                    IdOs = Convert.ToInt32(reader["Id_os"]),
                                    Entity = reader["Offshore_Entity"].ToString(),
                                    Jurisdiction = reader["Jurisdiction"].ToString(),
                                    LinkedTo = reader["Linked_to"].ToString(),
                                    DataFrom = reader["Data_from"].ToString()
                                },

                                cliente = new Cliente
                                {
                                    IdCliente = Convert.ToInt32(reader["Id_cliente"]),
                                    Nombre = reader["Cliente_Nombre"].ToString(),
                                    Apellido = reader["Cliente_Apellido"].ToString(),
                                    Estado = reader["Cliente_Estado"].ToString(),
                                    Correo = reader["Cliente_Correo"].ToString(),
                                    users = new Users
                                    {
                                        IdUser = Convert.ToInt32(reader["Id_user"]),
                                        Username = reader["Username"].ToString(),
                                        Password = reader["Password"].ToString()
                                    }
                                },

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
                    cmd.Parameters.Add(new SqlParameter("@Id_proveedor", objeto.proveedor.IdProveedor));
                    cmd.Parameters.Add(new SqlParameter("@Id_os", objeto.offshore.IdOs));
                    cmd.Parameters.Add(new SqlParameter("@Id_wbs", objeto.theWorldBankSource.IdWbs));
                    cmd.Parameters.Add(new SqlParameter("@Id_ofac", objeto.offacSource.IdOfac));
                    cmd.Parameters.Add(new SqlParameter("@Id_cliente", objeto.cliente.IdCliente));

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
                    cmd.Parameters.Add(new SqlParameter("@Id_proveedor", objeto.proveedor.IdProveedor));
                    cmd.Parameters.Add(new SqlParameter("@Id_os", objeto.offshore.IdOs));
                    cmd.Parameters.Add(new SqlParameter("@Id_wbs", objeto.theWorldBankSource.IdWbs));
                    cmd.Parameters.Add(new SqlParameter("@Id_ofac", objeto.offacSource.IdOfac));
                    cmd.Parameters.Add(new SqlParameter("@Id_cliente", objeto.cliente.IdCliente));

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
