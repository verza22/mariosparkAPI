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


        [HttpPost("RemoveProduct")]
        public IActionResult RemoveProduct([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "productID", typeof(int) }
                });
                int productID = (int)parameters["productID"];

                Product product = _productBusinessLogic.GetProduct(productID);
                bool isDeleted = _productBusinessLogic.RemoveProduct(productID);

                if (isDeleted && !string.IsNullOrEmpty(product.Image))
                {
                    Util.RemoveImage(product.Image);
                }

                return Ok(isDeleted);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOrUpdateProduct")]
        public async Task<IActionResult> AddOrUpdateProduct(
            [FromForm] int productID, 
            [FromForm] string name,
            [FromForm] string description,
            [FromForm] decimal price,
            [FromForm] int categoryId,
            [FromForm] int storeID, 
            [FromForm] bool changeImage, 
            IFormFile? image)
        {
            try
            {
                if (changeImage && (image == null || image.Length == 0))
                    return BadRequest("No image provided.");

                Product product = _productBusinessLogic.GetProduct(productID);
                string imageUrl = product.Image;

                if (productID > 0 && changeImage)
                {
                    Util.RemoveImage(imageUrl);
                }

                if (changeImage)
                {
                    var fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{storeID}_{Guid.NewGuid().ToString()}.jpg";

                    var folderPath = Path.Combine("wwwroot", "images");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var filePath = Path.Combine(folderPath, fileName);

                    var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);
                    stream.Close();

                    imageUrl = Path.Combine("images", fileName);
                }

                product.Name = name;
                product.Description = description;
                product.Price = price;
                product.CategoryId = categoryId;
                product.StoreId = storeID;
                product.Image = imageUrl;

                bool isAdded = _productBusinessLogic.AddOrUpdateProduct(product);

                return Ok(isAdded);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
