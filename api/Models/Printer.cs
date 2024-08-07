namespace api.Models
{
    public class Printer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Ip { get; set; }

        public bool IsPrincipal { get; set; }

        public string MessageIni { get; set; }

        public string MessageFin { get; set; }

        public int StoreID { get; set; }
    }
}
