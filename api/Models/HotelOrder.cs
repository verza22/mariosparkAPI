﻿namespace api.Models
{
    public class HotelOrder
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Total { get; set; }

        public DateTime DateIn { get; set; }

        public DateTime DateOut { get; set; }

        public string PaymentMethod { get; set; }

        public int People { get; set; }

        public int StoreId { get; set; }

        public HotelRoom Room { get; set; }

        public Customer Customer { get; set; }

    }
}
