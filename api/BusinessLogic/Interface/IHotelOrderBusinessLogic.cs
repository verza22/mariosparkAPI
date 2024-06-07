using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface IHotelOrderBusinessLogic
    {
        List<HotelOrder> GetHotelOrders(int store_id);
    }
}
