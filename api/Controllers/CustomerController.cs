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
    }
}
