using System;
using System.Collections.Generic;

namespace EYProveedores.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string RazonSocial { get; set; }

    public string Nombre { get; set; }

    public int IdTributaria { get; set; }

    public int Telefono { get; set; }

    public string Correo { get; set; }

    public string SitioWeb { get; set; }

    public string Direccion { get; set; }

    public string Pais { get; set; }

    public decimal Facturacion { get; set; }

    public DateTime UltimaEdicion { get; set; }

    public virtual ICollection<ListaScreening> ListaScreening { get; set; }

}
