using api.BusinessLogic.Interface;
using api.Lib;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusinessLogic _authBusinessLogic;
        public AuthController(IAuthBusinessLogic authBusinessLogic)
        {
            _authBusinessLogic = authBusinessLogic;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>
                {
                    { "userName", typeof(string) },
                    { "password", typeof(string) }
                });

                string userName = (string)parameters["userName"];
                string password = (string)parameters["password"];

                User user = _authBusinessLogic.Login(userName, password);

                if (user == null || user.UserId == 0)
                    return Ok(user);

                var token = _authBusinessLogic.Authenticate(userName, user.UserId);

                user.Token = token;

                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}