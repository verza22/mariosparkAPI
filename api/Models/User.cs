namespace api.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public int UserTypeId { get; set; }

        public int? OwnerId { get; set; }
    }
}
