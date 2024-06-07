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
    public class HotelOrderController : ControllerBase
    {
        private readonly IHotelOrderBusinessLogic _hotelOrderBusinessLogic;

        public HotelOrderController(IHotelOrderBusinessLogic hotelOrderBusinessLogic)
        {
            _hotelOrderBusinessLogic = hotelOrderBusinessLogic;
        }

        [HttpPost("GetHotelOrders")]
        public IActionResult GetHotelOrders([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "storeID", typeof(int) }
                });

                int storeID = (int)parameters["storeID"];

                List<HotelOrder> orders = _hotelOrderBusinessLogic.GetHotelOrders(storeID);

                return Ok(orders);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
