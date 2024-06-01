using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        // GET: HelloController
        [HttpGet]
        public string Get()
        {
            return "test";
        }

        [HttpGet("test")]
        public string test()
        {
            return "test 2000";
        }
    }
}
