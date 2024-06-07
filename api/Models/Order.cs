namespace api.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CashierId { get; set; }

        public int TableNumber { get; set; }

        public int WaiterId { get; set; }

        public int ChefId { get; set; }

        public decimal Total { get; set; }

        public DateTime Date { get; set; }

        public string PaymentMethod { get; set; }

        public int OrderStatusId { get; set; }

        public int StoreId { get; set; }

        public Customer Customer { get; set; }

        public List<Product> Products { get; set; }
    }
}
