using api.BusinessLogic.Interface;
using api.DataAccess;
using api.Models;

namespace api.BusinessLogic
{
    public class HotelRoomBusinessLogic : IHotelRoomBusinessLogic
    {
        private readonly HotelRoomDataAccess _hotelRoomDataAccess;

        public HotelRoomBusinessLogic(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _hotelRoomDataAccess = new HotelRoomDataAccess(connectionString);
        }

        public List<HotelRoom> GetHotelRooms(int store_id)
        {
            return _hotelRoomDataAccess.GetHotelRooms(store_id);
        }
    }
}
