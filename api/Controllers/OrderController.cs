using api.BusinessLogic;
using api.BusinessLogic.Interface;
using api.Lib;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusinessLogic _orderBusinessLogic;
        private readonly IUserBusinessLogic _userBusinessLogic;
        private readonly INotificationBusinessLogic _notificationBusinessLogic;

        public OrderController(
            IOrderBusinessLogic orderBusinessLogic,
            IUserBusinessLogic userBusinessLogic,
            INotificationBusinessLogic notificationBusinessLogic
         )
        {
            _orderBusinessLogic = orderBusinessLogic;
            _userBusinessLogic = userBusinessLogic;
            _notificationBusinessLogic = notificationBusinessLogic;
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

                //test
                List<string> tokenList = _userBusinessLogic.GetUserTokenByStore(storeID);

                string response = _notificationBusinessLogic.SendNotification(tokenList, "titulo prueba", "mensaje prueba");

                return Ok(orders);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertOrder")]
        public async Task<IActionResult> InsertOrder([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "cashierID", typeof(int) },
                    { "tableNumber", typeof(int) },
                    { "waiterID", typeof(int) },
                    { "chefID", typeof(int) },
                    { "total", typeof(decimal) },
                    { "date", typeof(DateTime) },
                    { "paymentMethod", typeof(string) },
                    { "orderStatus", typeof(int) },
                    { "storeID", typeof(int) },
                    { "customer", typeof(string) },
                    { "products", typeof(string) }
                });

                Order order = new Order();

                order.CashierId = (int)parameters["cashierID"];
                order.TableNumber = (int)parameters["tableNumber"];
                order.WaiterId = (int)parameters["waiterID"];
                order.ChefId = (int)parameters["chefID"];
                order.Total = (decimal)parameters["total"];
                order.Date = (DateTime)parameters["date"];
                order.PaymentMethod = (string)parameters["paymentMethod"];
                order.OrderStatusId = (int)parameters["orderStatus"];
                order.StoreId = (int)parameters["storeID"];
                order.Customer = JsonConvert.DeserializeObject<Customer>((string)parameters["customer"]);
                order.Products = JsonConvert.DeserializeObject<List<Product>>((string)parameters["products"]);

                int result = _orderBusinessLogic.InsertOrder(order);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SyncOrders")]
        public async Task<IActionResult> SyncOrders([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "data", typeof(string) }
                });

                string data = (string)parameters["data"];

                var orders = JsonConvert.DeserializeObject<List<Order>>(data);

                bool result = true;


                foreach (Order order in orders)
                {
                    //validations
                    if (order == null)
                    {
                        return BadRequest("Invalid order in the list.");
                    }

                    //insert data
                    int resultAux = _orderBusinessLogic.InsertOrder(order);

                    if (resultAux == 0) {
                        result = false;
                    }
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
