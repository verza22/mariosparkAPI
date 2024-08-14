using api.BusinessLogic;
using api.BusinessLogic.Interface;
using api.Lib;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly IUserBusinessLogic _userBusinessLogic;
        private readonly INotificationBusinessLogic _notificationBusinessLogic;

        public HotelOrderController(
            IHotelOrderBusinessLogic hotelOrderBusinessLogic,
            IUserBusinessLogic userBusinessLogic,
            INotificationBusinessLogic notificationBusinessLogic
        )
        {
            _hotelOrderBusinessLogic = hotelOrderBusinessLogic;
            _userBusinessLogic = userBusinessLogic;
            _notificationBusinessLogic = notificationBusinessLogic;
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

        [HttpPost("RemoveHotelOrder")]
        public IActionResult RemoveHotelOrder([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "orderID", typeof(int) }
                });
                int orderID = (int)parameters["orderID"];

                bool isDeleted = _hotelOrderBusinessLogic.RemoveHotelOrder(orderID);

                return Ok(isDeleted);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOrUpdateHotelOrder")]
        public async Task<IActionResult> AddOrUpdateHotelOrder([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "orderID", typeof(int) },
                    { "userID", typeof(int) },
                    { "total", typeof(decimal) },
                    { "dateIN", typeof(DateTime) },
                    { "dateOUT", typeof(DateTime) },
                    { "paymentMethod", typeof(string) },
                    { "people", typeof(int) },
                    { "storeID", typeof(int) },
                    { "cantBabies", typeof(int) },
                    { "cantChildren", typeof(int) },
                    { "cantAdult", typeof(int) },
                    { "customer", typeof(string) },
                    { "room", typeof(string) }
                });

                HotelOrder order = new HotelOrder();

                order.Id = (int)parameters["orderID"];
                order.UserId = (int)parameters["userID"];
                order.Total = (decimal)parameters["total"];
                order.DateIN = (DateTime)parameters["dateIN"];
                order.DateOUT = (DateTime)parameters["dateOUT"];
                order.PaymentMethod = (string)parameters["paymentMethod"];
                order.People = (int)parameters["people"];
                order.StoreId = (int)parameters["storeID"];
                order.CantBabies = (int)parameters["cantBabies"];
                order.CantChildren = (int)parameters["cantChildren"];
                order.CantAdult = (int)parameters["cantAdult"];
                order.Customer = JsonConvert.DeserializeObject<Customer>((string)parameters["customer"]);
                order.Room = JsonConvert.DeserializeObject<HotelRoom>((string)parameters["room"]);

                int result = _hotelOrderBusinessLogic.AddOrUpdateHotelOrder(order);

                if (result > 0 && order.Id == 0)
                {
                    string title = "Nueva venta en hospedaje";
                    string message = "$ " + order.Total + ", cliente: " + order.Customer.Name;

                    List<string> tokenList = _userBusinessLogic.GetUserTokenByStore(order.StoreId);
                    string response = _notificationBusinessLogic.SendNotification(tokenList, title, message);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
