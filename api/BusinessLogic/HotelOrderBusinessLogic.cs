using api.BusinessLogic.Interface;
using api.DataAccess;
using api.Models;

namespace api.BusinessLogic
{
    public class HotelOrderBusinessLogic : IHotelOrderBusinessLogic
    {
        private readonly HotelOrderDataAccess _hotelOrderDataAccess;

        public HotelOrderBusinessLogic(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _hotelOrderDataAccess = new HotelOrderDataAccess(connectionString);
        }

        public List<HotelOrder> GetHotelOrders(int store_id)
        {
            return _hotelOrderDataAccess.GetHotelOrders(store_id);
        }

        public bool RemoveHotelOrder(int orderID)
        {
            return _hotelOrderDataAccess.RemoveHotelOrder(orderID);
        }

        public bool AddOrUpdateHotelOrder(HotelOrder order)
        {
            return _hotelOrderDataAccess.AddOrUpdateHotelOrder(order);
        }
    }
}
