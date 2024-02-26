using System;
using System.Collections.Generic;

namespace EYProveedores.Models;

public partial class Role
{
    public int IdRol { get; set; }

    public string Rol { get; set; }

    public string Descripcion { get; set; }

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; }
}
