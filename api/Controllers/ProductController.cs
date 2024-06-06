using api.BusinessLogic;
using api.BusinessLogic.Interface;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductBusinessLogic _productBusinessLogic;
        private readonly ICategoryBusinessLogic _categoryBusinessLogic;

        public ProductController(IProductBusinessLogic productBusinessLogic, ICategoryBusinessLogic categoryBusinessLogic)
        {
            _productBusinessLogic = productBusinessLogic;
            _categoryBusinessLogic = categoryBusinessLogic;
        }

        [HttpPost("GetProducts")]
        public IActionResult GetProducts([FromBody] JsonElement requestBody)
        {
            if (!requestBody.TryGetProperty("storeID", out var storeIDElement) ||
               !requestBody.TryGetProperty("categoryID", out var categoryIDElement) ||
               !requestBody.TryGetProperty("needCategories", out var needCategoriesElement))
            {
                return BadRequest("Invalid JSON format. 'storeID', 'categoryID' and 'needCategories' are required.");
            }

            int storeID = storeIDElement.GetInt32();
            int categoryID = categoryIDElement.GetInt32();
            bool needCategories = needCategoriesElement.GetBoolean();

            List<Product> products = _productBusinessLogic.GetProducts(storeID, categoryID);
            List<Category> categories = new List<Category>();

            if(needCategories)
                categories = _categoryBusinessLogic.GetCategoriesByStoreId(storeID);

            return Ok(new { products, categories });
        }
    }
}
