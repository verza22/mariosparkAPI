using api.BusinessLogic;
using api.BusinessLogic.Interface;
using api.Lib;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HotelRoomController : ControllerBase
    {
        private readonly IHotelRoomBusinessLogic _hotelRoomBusinessLogic;

        public HotelRoomController(IHotelRoomBusinessLogic hotelRoomBusinessLogic)
        {
            _hotelRoomBusinessLogic = hotelRoomBusinessLogic;
        }

        [HttpPost("GetHotelRooms")]
        public IActionResult GetHotelRooms([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "storeID", typeof(int) }
                });

                int storeID = (int)parameters["storeID"];

                List<HotelRoom> rooms = _hotelRoomBusinessLogic.GetHotelRooms(storeID);

                return Ok(rooms);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RemoveHotelRoom")]
        public IActionResult RemoveHotelRoom([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "roomID", typeof(int) }
                });
                int roomID = (int)parameters["roomID"];

                bool isDeleted = _hotelRoomBusinessLogic.RemoveHotelRoom(roomID);

                return Ok(isDeleted);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOrUpdateHotelRoom")]
        public async Task<IActionResult> AddOrUpdateHotelRoom([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "id", typeof(int) },
                    { "name", typeof(string) },
                    { "capacity", typeof(int) },
                    { "roomType", typeof(int) },
                    { "storeID", typeof(int) }
                });

                HotelRoom room = new HotelRoom();

                room.Id = (int)parameters["id"];
                room.Name = (string)parameters["name"];
                room.Capacity = (int)parameters["capacity"];
                room.Type = (int)parameters["roomType"];
                room.StoreId = (int)parameters["storeID"];

                bool isAdded = _hotelRoomBusinessLogic.AddOrUpdateHotelRoom(room);

                return Ok(isAdded);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
