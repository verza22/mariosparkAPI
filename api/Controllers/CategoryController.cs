using api.BusinessLogic.Interface;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBusinessLogic _categoryBusinessLogic;

        public CategoryController(ICategoryBusinessLogic categoryBusinessLogic)
        {
            _categoryBusinessLogic = categoryBusinessLogic;
        }

        [HttpGet("{storeID}")]
        public IActionResult GetCategories(int storeID)
        {
            List<Category> categories = _categoryBusinessLogic.GetCategoriesByStoreId(storeID);
            return Ok(categories);
        }
    }
}
