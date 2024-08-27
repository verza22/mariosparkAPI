using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface IAuthBusinessLogic
    {
        User Login(string userName, string password);
        string Authenticate(string userName, int userID);
        List<UserType> GetUserTypes();
        List<OrderStatus> GetOrderStatus();
        List<HotelOrderType> GetHotelOrderTypes();
        List<HotelRoomType> GetHotelRoomTypes(int store_id);
        List<UserConfig> GetUserConfig(int userID);
    }
}
