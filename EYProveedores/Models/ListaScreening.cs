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

    public virtual Cliente Cliente { get; set; }
    public virtual Offacsource OFACSource {get; set; }
    public virtual OffshoreSource OffshoreSource { get; set; }
    public virtual Proveedor Proveedor { get; set; }
    public virtual TheWorldBankSource TheWorldBankSource { get; set; }
}
