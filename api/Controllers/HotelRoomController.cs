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
        public async Task<IActionResult> AddOrUpdateHotelRoom(
            [FromForm] int id,
            [FromForm] string name,
            [FromForm] int capacity,
            [FromForm] int roomType,
            [FromForm] int storeID,
            [FromForm] decimal priceBabies,
            [FromForm] decimal priceChildren,
            [FromForm] decimal priceAdults,
            [FromForm] string description,
            [FromForm] bool changeImage,
            IFormFile? image)
        {
            try
            {
                if (changeImage && (image == null || image.Length == 0))
                    return BadRequest("No image provided.");

                HotelRoom room = _hotelRoomBusinessLogic.GetHotelRoom(id);
                string imageUrl = room.Image;

                if (id > 0 && changeImage)
                {
                    Util.RemoveImage(imageUrl);
                }

                if (changeImage)
                {
                    var fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{storeID}_{Guid.NewGuid().ToString()}.jpg";

                    var folderPath = Path.Combine("wwwroot", "images");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var filePath = Path.Combine(folderPath, fileName);

                    var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);
                    stream.Close();

                    imageUrl = Path.Combine("images", fileName);
                }

                room.Id = id;
                room.Name = name;
                room.Capacity = capacity;
                room.Type = roomType;
                room.StoreId = storeID;
                room.PriceBabies = priceBabies;
                room.PriceChildren = priceChildren;
                room.PriceAdults = priceAdults;
                room.Image = imageUrl;
                room.Description = description;

                int result = _hotelRoomBusinessLogic.AddOrUpdateHotelRoom(room);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
