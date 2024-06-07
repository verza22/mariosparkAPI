using api.BusinessLogic.Interface;
using api.DataAccess;
using api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.BusinessLogic
{
    public class AuthBusinessLogic : IAuthBusinessLogic
    {
        private readonly IConfiguration _configuration;
        private readonly AuthDataAccess _authDataAccess;

        public AuthBusinessLogic(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _configuration = configuration;
            _authDataAccess = new AuthDataAccess(connectionString);
        }

        public User Login(string userName, string password)
        {
            return _authDataAccess.Login(userName, password);
        }

        public List<UserType> GetUserTypes()
        {
            return _authDataAccess.GetUserTypes();
        }

        public List<OrderStatus> GetOrderStatus()
        {
            return _authDataAccess.GetOrderStatus();
        }

        public List<HotelOrderType> GetHotelOrderTypes()
        {
            return _authDataAccess.GetHotelOrderTypes();
        }

        public List<HotelRoomType> GetHotelRoomTypes(int store_id)
        {
            return _authDataAccess.GetHotelRoomTypes(store_id);
        }

        public string Authenticate(string userName, int userID)
        {
            var key = _configuration.GetValue<string>("JwtConfig:Key");
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                 {
                 new Claim(ClaimTypes.Name, userName),
                 new Claim(ClaimTypes.NameIdentifier, userID.ToString())
                 }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                                      new SymmetricSecurityKey(keyBytes),
                                      SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
