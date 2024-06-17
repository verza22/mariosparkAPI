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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBusinessLogic _customerBusinessLogic;

        public CustomerController(ICustomerBusinessLogic customerBusinessLogic)
        {
            _customerBusinessLogic = customerBusinessLogic;
        }

        [HttpPost("GetCustomers")]
        public IActionResult GetCustomers([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "storeID", typeof(int) }
                });

                int storeID = (int)parameters["storeID"];

                List<Customer> customers = _customerBusinessLogic.GetCustomers(storeID);

                return Ok(customers);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RemoveCustomer")]
        public IActionResult RemoveCustomer([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "customerID", typeof(int) }
                });
                int customerID = (int)parameters["customerID"];

                bool isDeleted = _customerBusinessLogic.RemoveCustomer(customerID);

                return Ok(isDeleted);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOrUpdateCustomer")]
        public async Task<IActionResult> AddOrUpdateCustomer([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "id", typeof(int) },
                    { "name", typeof(string) },
                    { "dni", typeof(string) },
                    { "email", typeof(string) },
                    { "phone", typeof(string) },
                    { "address", typeof(string) },
                    { "storeID", typeof(int) }
                });

                Customer customer = new Customer();

                customer.Id = (int)parameters["id"];
                customer.Name = (string)parameters["name"];
                customer.Dni = (string)parameters["dni"];
                customer.Email = (string)parameters["email"];
                customer.Phone = (string)parameters["phone"];
                customer.Address = (string)parameters["address"];
                customer.StoreId = (int)parameters["storeID"];

                int result = _customerBusinessLogic.AddOrUpdateCustomer(customer);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
