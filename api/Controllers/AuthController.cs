using api.BusinessLogic.Interface;
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
            if (!requestBody.TryGetProperty("userName", out var userNameElement) ||
                !requestBody.TryGetProperty("password", out var passwordElement))
            {
                return BadRequest("Invalid JSON format. 'userName' and 'password' are required.");
            }

            var userName = userNameElement.GetString();
            var password = passwordElement.GetString();

            User user = _authBusinessLogic.Login(userName, password);

            if (user == null || user.UserId == 0)
                return Ok(user);

            var token = _authBusinessLogic.Authenticate(userName, user.UserId);

            user.Token = token;

            return Ok(user);
        }
    }
}