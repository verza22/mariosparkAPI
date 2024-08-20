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

        public HotelRoom GetHotelRoom(int id)
        {
            return _hotelRoomDataAccess.GetHotelRoom(id);
        }

        public List<HotelRoom> GetHotelRooms(int store_id)
        {
            return _hotelRoomDataAccess.GetHotelRooms(store_id);
        }

        public bool RemoveHotelRoom(int roomID)
        {
            return _hotelRoomDataAccess.RemoveHotelRoom(roomID);
        }

        public int AddOrUpdateHotelRoom(HotelRoom room)
        {
            return _hotelRoomDataAccess.AddOrUpdateHotelRoom(room);
        }
    }
}
