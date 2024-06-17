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
    public class UserController : ControllerBase
    {
        private readonly IUserBusinessLogic _userBusinessLogic;

        public UserController(IUserBusinessLogic userBusinessLogic)
        {
            _userBusinessLogic = userBusinessLogic;
        }

        [HttpPost("GetUsers")]
        public IActionResult GetUsers([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "storeID", typeof(int) }
                });

                int storeID = (int)parameters["storeID"];

                List<User> users = _userBusinessLogic.GetUsers(storeID);

                return Ok(users);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RemoveUser")]
        public IActionResult RemoveUser([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "userID", typeof(int) }
                });
                int userID = (int)parameters["userID"];

                bool isDeleted = _userBusinessLogic.RemoveUser(userID);

                return Ok(isDeleted);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOrUpdateUser")]
        public async Task<IActionResult> AddOrUpdateUser([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "id", typeof(int) },
                    { "user", typeof(string) },
                    { "name", typeof(string) },
                    { "password", typeof(string) },
                    { "userType", typeof(int) },
                    { "storeID", typeof(int) }
                });

                User user = new User();

                user.Id = (int)parameters["id"];
                user.Username = (string)parameters["user"];
                user.Name = (string)parameters["name"];
                user.Password = (string)parameters["password"];
                user.Type = (int)parameters["userType"];
                user.DefaultStoreID = (int)parameters["storeID"];

                int result = _userBusinessLogic.AddOrUpdateUser(user);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
