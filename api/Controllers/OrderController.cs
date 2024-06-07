using api.BusinessLogic.Interface;
using api.Lib;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusinessLogic _orderBusinessLogic;

        public OrderController(IOrderBusinessLogic orderBusinessLogic)
        {
            _orderBusinessLogic = orderBusinessLogic;
        }

        [HttpPost("GetOrders")]
        public IActionResult GetOrders([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "storeID", typeof(int) }
                });

                int storeID = (int)parameters["storeID"];

                List<Order> orders = _orderBusinessLogic.GetOrders(storeID);

                return Ok(orders);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
