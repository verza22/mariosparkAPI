﻿namespace api.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public int Type { get; set; }

        public int DefaultStoreID { get; set; }
    }
}
