using System;
using System.Collections.Generic;

namespace EYProveedores.Models;

public partial class TheWorldBankSource
{
    public int IdWbs { get; set; }

    public string FirmName { get; set; }

    public string Address { get; set; }

    public string Country { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string Grounds { get; set; }

    public virtual ICollection<ListaScreening> ListaScreening { get; set; }

}
