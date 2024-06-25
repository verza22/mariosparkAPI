using api.BusinessLogic.Interface;
using api.DataAccess;
using api.Models;

namespace api.BusinessLogic
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private readonly UserDataAccess _userDataAccess;

        public UserBusinessLogic(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _userDataAccess = new UserDataAccess(connectionString);
        }

        public List<User> GetUsers(int store_id)
        {
            return _userDataAccess.GetUsers(store_id);
        }

        public bool RemoveUser(int userID)
        {
            return _userDataAccess.RemoveUser(userID);
        }

        public int AddOrUpdateUser(User user)
        {
            return _userDataAccess.AddOrUpdateUser(user);
        }

        public int UpdateUserToken(int userID, string token)
        {
            return _userDataAccess.UpdateUserToken(userID, token);
        }

        public List<string> GetUserTokenByStore(int storeID)
        {
            return _userDataAccess.GetUserTokenByStore(storeID);
        }
    }
}
