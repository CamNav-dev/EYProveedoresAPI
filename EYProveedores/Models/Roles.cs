namespace EYProveedores.Models
{
    public class Roles
    {
        public int IdRol { get; set; }
        public int Rol { get; set; }
        public int Descripcion { get; set; }
        public Users users { get; set; }
        public List<Users> listUsers { get; set; }
    }
}
