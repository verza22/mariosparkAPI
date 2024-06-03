using api.BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
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
            return Ok(stores);
        }
    }
}