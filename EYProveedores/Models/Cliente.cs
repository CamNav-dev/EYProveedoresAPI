using System;
using System.Collections.Generic;

namespace EYProveedores.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int? IdUser { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Estado { get; set; }

    public string Correo { get; set; }

    public virtual User IdUserNavigation { get; set; }

    public virtual ICollection<ListaScreening> ListaScreenings { get; set; } = new List<ListaScreening>();
}
