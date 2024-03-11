using System;
using System.Collections.Generic;

namespace EYProveedores.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }
    public int IdUser { get; set; } // Clave Foránea
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Estado { get; set; }
    public string Correo { get; set; }

    // Propiedad de navegación hacia User
    public virtual User User { get; set; }
    public virtual ICollection<ListaScreening> ListaScreening { get; set; }

}
