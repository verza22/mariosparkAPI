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
            try
            {
                List<Category> categories = _categoryBusinessLogic.GetCategoriesByStoreId(storeID);
                return Ok(categories);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RemoveCategory")]
        public IActionResult RemoveCategory([FromBody] JsonElement requestBody)
        {
            try
            {
                var parameters = Util.ValidateRequest(requestBody, new Dictionary<string, Type>{
                    { "categoryID", typeof(int) }
                });
                int categoryID = (int)parameters["categoryID"];

                Category category = _categoryBusinessLogic.GetCategory(categoryID);
                bool isDeleted = _categoryBusinessLogic.RemoveCategory(categoryID);

                if (isDeleted && !string.IsNullOrEmpty(category.Image))
                {
                    Util.RemoveImage(category.Image);
                }

                return Ok(isDeleted);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOrUpdateCategory")]
        public async Task<IActionResult> AddOrUpdateCategory([FromForm] int categoryID, [FromForm] string name, [FromForm] int storeID, [FromForm] bool changeImage, IFormFile? image)
        {
            try
            {
                if (changeImage && (image == null || image.Length == 0))
                    return BadRequest("No image provided.");

                Category category = _categoryBusinessLogic.GetCategory(categoryID);
                string imageUrl = category.Image;

                if (categoryID > 0 && changeImage) 
                {
                    Util.RemoveImage(imageUrl);
                }

                if (changeImage) { 
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

                bool isAdded = _categoryBusinessLogic.AddOrUpdateCategory(categoryID, name, imageUrl, storeID);

                return Ok(isAdded);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
