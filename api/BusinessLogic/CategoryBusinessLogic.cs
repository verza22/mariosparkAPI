using api.BusinessLogic.Interface;
using api.DataAccess;
using api.Models;

namespace api.BusinessLogic
{
    public class CategoryBusinessLogic : ICategoryBusinessLogic
    {
        private readonly CategoryDataAccess _categoryDataAccess;

        public CategoryBusinessLogic(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _categoryDataAccess = new CategoryDataAccess(connectionString);
        }

        public List<Category> GetCategoriesByStoreId(int storeID)
        {
            return _categoryDataAccess.GetCategoriesByStoreId(storeID);
        }
        public Category GetCategory(int id)
        {
            return _categoryDataAccess.GetCategory(id);
        }

        public bool AddOrUpdateCategory(int categoryID, string name, string image, int storeID)
        {
            return _categoryDataAccess.AddOrUpdateCategory(categoryID, name, image, storeID);
        }

        public bool RemoveCategory(int categoryId)
        {
            return _categoryDataAccess.RemoveCategory(categoryId);
        }
    }
}
