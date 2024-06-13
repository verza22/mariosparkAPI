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

                bool isAdded = _orderBusinessLogic.InsertOrder(order);

                return Ok(isAdded);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
