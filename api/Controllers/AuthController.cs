using api.BusinessLogic.Interface;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login(string userName, string password)
        {
            User user = _authBusinessLogic.Login(userName, password);

            if (user == null || user.UserId == 0)
                return Ok(user);

            var token = _authBusinessLogic.Authenticate(userName, user.UserId);

            user.Token = token;

            return Ok(user);
        }
    }
}