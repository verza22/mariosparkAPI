namespace api.Models
{
    public class HotelRoom
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public int Type { get; set; }

        public int StoreId { get; set; }

        public decimal PriceBabies { get; set; }

        public decimal PriceChildren { get; set; }

        public decimal PriceAdults { get; set; }
        public string Image { get; set; }

        public string Description { get; set; }
    }
}
