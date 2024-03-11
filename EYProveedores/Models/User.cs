using System;
using System.Collections.Generic;

namespace EYProveedores.Models;

public partial class User
{
    public User()
    {
        Clientes = new HashSet<Cliente>();
    }

    public int IdUser { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    // Propiedad de navegación para la relación Uno a Muchos con Cliente
    public virtual ICollection<Cliente> Clientes { get; set; }
}
