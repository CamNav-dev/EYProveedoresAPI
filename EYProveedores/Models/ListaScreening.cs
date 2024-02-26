using System;
using System.Collections.Generic;

namespace EYProveedores.Models;

public partial class ListaScreening
{
    public int IdScreening { get; set; }

    public DateTime Fecha { get; set; }

    public string Tipo { get; set; }

    public int IdProveedor { get; set; }

    public int IdOs { get; set; }

    public int IdWbs { get; set; }

    public int IdOfac { get; set; }

    public int IdCliente { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; }

    public virtual Offacsource IdOfacNavigation { get; set; }

    public virtual OffshoreSource IdOsNavigation { get; set; }

    public virtual Proveedor IdProveedorNavigation { get; set; }

    public virtual TheWorldBankSource IdWbsNavigation { get; set; }
}
