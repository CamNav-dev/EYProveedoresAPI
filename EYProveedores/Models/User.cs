using System;
using System.Collections.Generic;

namespace EYProveedores.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
