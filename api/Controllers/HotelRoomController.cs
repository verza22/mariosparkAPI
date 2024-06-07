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
    }
}
