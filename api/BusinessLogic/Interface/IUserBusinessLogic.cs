using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface IUserBusinessLogic
    {
        User Login(string userName, string password);
    }
}
