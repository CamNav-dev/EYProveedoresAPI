namespace EYProveedores.Models
{
    public class ListaScreening
    {
        public int IdScreening { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public Proveedor proveedor { get; set; }
        public List<Proveedor> listProveedores { get; set; }
        public OffshoreSource offshore { get; set; }

        public List<OffshoreSource> listOffshoreSource { get; set; }
        public TheWorldBankSource theWorldBankSource { get; set; }
        public List<TheWorldBankSource> listTheWorldBankSource { get; set; }

        public OFFACSource offacSource { get; set; }
        public List<OFFACSource> listOFFACSource { get; set; }

        public Cliente cliente { get; set; }
        public List<Cliente> listCliente { get; set; }
    }
}
