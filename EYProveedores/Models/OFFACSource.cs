using System;
using System.Collections.Generic;

namespace EYProveedores.Models;

public partial class Offacsource
{
    public int IdOfac { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string Type { get; set; }

    public string Programs { get; set; }

    public string List { get; set; }

    public string Score { get; set; }

    public virtual ICollection<ListaScreening> ListaScreenings { get; set; } = new List<ListaScreening>();
}
