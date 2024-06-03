namespace api.Models
{
    public class HotelOrder
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public decimal Total { get; set; }

        public DateTime DateIn { get; set; }

        public DateTime DateOut { get; set; }

        public string PaymentMethod { get; set; }

        public int People { get; set; }

        public string Room { get; set; }

        public string Customer { get; set; }

        public int StoreId { get; set; }
    }
}
