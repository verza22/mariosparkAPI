using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface IHotelRoomBusinessLogic
    {
        List<HotelRoom> GetHotelRooms(int store_id);
        bool RemoveHotelRoom(int roomID);
        bool AddOrUpdateHotelRoom(HotelRoom room);
    }
}
