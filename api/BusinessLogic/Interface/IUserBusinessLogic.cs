using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface IUserBusinessLogic
    {
        List<User> GetUsers(int store_id);
        bool RemoveUser(int userID);
        int AddOrUpdateUser(User user);
    }
}
