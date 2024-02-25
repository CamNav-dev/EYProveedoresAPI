namespace EYProveedores.Models
{
    public class TheWorldBankSource
    {
        public int IdWbs { get; set; }
        public string Firm_name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Grounds { get; set; }
    }
}
