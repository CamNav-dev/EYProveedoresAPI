using System;
using System.Collections.Generic;

namespace EYProveedores.Models;

public partial class OffshoreSource
{
    public int IdOs { get; set; }

    public string Entity { get; set; }

    public string Jurisdiction { get; set; }

    public string LinkedTo { get; set; }

    public string DataFrom { get; set; }

    public virtual ICollection<ListaScreening> ListaScreenings { get; set; } = new List<ListaScreening>();
}
