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
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "storeID", typeof(int) },
                    { "categoryID", typeof(int) },
                    { "needCategories", typeof(bool) }
                });

                int storeID = (int)parameters["storeID"];
                int categoryID = (int)parameters["categoryID"];
                bool needCategories = (bool)parameters["needCategories"];

                List<Product> products = _productBusinessLogic.GetProducts(storeID, categoryID);
                List<Category> categories = new List<Category>();

                if(needCategories)
                    categories = _categoryBusinessLogic.GetCategoriesByStoreId(storeID);

                return Ok(new { products, categories });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
