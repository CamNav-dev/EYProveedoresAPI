using System;
using System.Collections.Generic;

namespace EYProveedores.Models;

public partial class TheWorldBankSource
{
    public int IdWbs { get; set; }

    public string FirmName { get; set; }

    public string Address { get; set; }

    public string Country { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public string Grounds { get; set; }

    public virtual ICollection<ListaScreening> ListaScreenings { get; set; } = new List<ListaScreening>();
}
