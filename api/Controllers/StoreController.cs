using api.BusinessLogic.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class StoreController : ControllerBase
    {
        private readonly IStoreBusinessLogic _storeBusinessLogic;

        public StoreController(IStoreBusinessLogic storeBusinessLogic)
        {
            _storeBusinessLogic = storeBusinessLogic;
        }

        [HttpGet("{ownerId}")]
        public IActionResult GetStoresByOwnerId(int ownerId)
        {
            var stores = _storeBusinessLogic.GetStoresByOwnerId(ownerId);

            var userName = User.Identity.Name;
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(id);
        }
    }
}