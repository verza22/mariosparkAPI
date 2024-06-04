using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface IAuthBusinessLogic
    {
        User Login(string userName, string password);
        string Authenticate(string userName, int userID);
    }
}
